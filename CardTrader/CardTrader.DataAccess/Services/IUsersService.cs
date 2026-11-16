using System.ComponentModel.DataAnnotations;
using CardTrader.DataAccess.Exceptions;
using CardTrader.DataAccess.Models;

namespace CardTrader.DataAccess.Services;

/// <summary>
/// The result of a successful login or token refresh.
/// </summary>
public record LoginResult(string UserId, string AccessToken, string RefreshToken);

public interface IUsersService
{
    /// <summary>
    /// Registers a new user with the given password.
    /// </summary>
    /// <exception cref="DuplicateUserException" />
    /// <exception cref="ValidationException" />
    Task AddUserAsync(User user, string password);

    /// <summary>
    /// Logs in the user and issues a new token pair.
    /// </summary>
    /// <exception cref="InvalidCredentialsException" />
    /// <exception cref="UserLockedOutException" />
    /// <exception cref="SaveFailedException" />
    Task<LoginResult> LoginAsync(string email, string password);

    /// <summary>
    /// Issues a new token pair in exchange for a valid refresh token.
    /// </summary>
    /// <exception cref="InvalidRefreshTokenException" />
    /// <exception cref="SaveFailedException" />
    Task<LoginResult> RedeemRefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Invalidates the refresh token of the current user. Does nothing if no user is logged in.
    /// </summary>
    /// <exception cref="SaveFailedException" />
    Task LogoutAsync();

    /// <summary>
    /// Returns the user with the given id from the database.
    /// </summary>
    /// <exception cref="AccessDeniedException" />
    Task<User> GetUserByIdAsync(string id);

    /// <summary>
    /// Returns the id of the current user from the access token claims, without querying the database,
    /// or <see langword="null" /> if no user is logged in.
    /// </summary>
    string? GetCurrentUserId();

    /// <summary>
    /// Loads the current user from the database, or returns <see langword="null" /> if no user is logged in.
    /// </summary>
    Task<User?> GetCurrentUserAsync();
}
