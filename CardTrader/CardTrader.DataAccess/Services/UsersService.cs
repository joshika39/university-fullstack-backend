using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using CardTrader.DataAccess.Config;
using CardTrader.DataAccess.Exceptions;
using CardTrader.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CardTrader.DataAccess.Services;

internal class UsersService : IUsersService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UsersService(
        IOptions<JwtSettings> jwtSettings,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _jwtSettings = jwtSettings.Value;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Authentication

    /// <inheritdoc />
    public async Task AddUserAsync(User user, string password)
    {
        // Create user
        var result = await _userManager.CreateAsync(user, password);

        // Check duplicate
        var isDuplicate = result.Errors.Any(e =>
            e.Code is nameof(IdentityErrorDescriber.DuplicateEmail)
                or nameof(IdentityErrorDescriber.DuplicateUserName));
        if (isDuplicate)
            throw new DuplicateUserException();

        // Check other errors
        if (!result.Succeeded)
            throw new ValidationException(string.Join(" ", result.Errors.Select(e => e.Description)));
    }

    /// <inheritdoc />
    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        // Find user (by email)
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new InvalidCredentialsException();

        // Check password and lockout
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        if (result.IsLockedOut)
            throw new UserLockedOutException();
        if (!result.Succeeded)
            throw new InvalidCredentialsException();

        // Issue tokens
        var accessToken = GenerateAccessToken(user);
        var refreshToken = await RotateRefreshTokenAsync(user);

        return new LoginResult(user.Id, accessToken, refreshToken);
    }

    /// <inheritdoc />
    public async Task<LoginResult> RedeemRefreshTokenAsync(string refreshToken)
    {
        // Parse refresh token
        if (!Guid.TryParse(refreshToken, out var parsedToken))
            throw new InvalidRefreshTokenException();

        // Find user (by refresh token)
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == parsedToken);
        if (user is null)
            throw new InvalidRefreshTokenException();

        // Issue tokens
        var accessToken = GenerateAccessToken(user);
        var newRefreshToken = await RotateRefreshTokenAsync(user);

        return new LoginResult(user.Id, accessToken, newRefreshToken);
    }

    /// <inheritdoc />
    public async Task LogoutAsync()
    {
        // Find current user
        var user = await GetCurrentUserAsync();
        if (user is null)
            return;

        // Clear refresh token
        user.RefreshToken = null;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new SaveFailedException("Refresh token could not be cleared");
    }

    #endregion

    #region Users

    /// <inheritdoc />
    public async Task<User> GetUserByIdAsync(string id)
    {
        // Check access
        if (id != GetCurrentUserId())
            throw new AccessDeniedException();

        // Find user (by id)
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new AccessDeniedException();

        return user;
    }

    #endregion

    #region Current user

    /// <inheritdoc />
    public string? GetCurrentUserId() =>
        _httpContextAccessor.HttpContext?.User.FindFirstValue("id");

    /// <inheritdoc />
    public async Task<User?> GetCurrentUserAsync()
    {
        // Get current user id
        var userId = GetCurrentUserId();
        if (userId is null)
            return null;

        // Find user (by id)
        return await _userManager.FindByIdAsync(userId);
    }

    #endregion

    #region Helpers

    private async Task<string> RotateRefreshTokenAsync(User user)
    {
        // Set and save new refresh token
        user.RefreshToken = Guid.NewGuid();
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new SaveFailedException("Refresh token could not be saved");

        return user.RefreshToken.Value.ToString();
    }

    private string GenerateAccessToken(User user)
    {
        // Claims
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("id", user.Id),
            new("username", user.UserName!),
        };

        // Signing
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }

    #endregion
}
