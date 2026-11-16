using CardTrader.DataAccess.Models;
using CardTrader.DataAccess.Services;
using CardTrader.Shared.Dtos;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardTrader.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public AuthController(IMapper mapper, IUsersService usersService)
    {
        _mapper = mapper;
        _usersService = usersService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] UserRequestDto userRequestDto)
    {
        var user = _mapper.Map<User>(userRequestDto);

        await _usersService.AddUserAsync(user, userRequestDto.Password);

        var userResponseDto = _mapper.Map<UserResponseDto>(user);

        return CreatedAtAction(
            nameof(UsersController.GetUserById),
            "Users",
            new { id = user.Id },
            userResponseDto);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var loginResult = await _usersService.LoginAsync(loginRequestDto.Email, loginRequestDto.Password);

        return _mapper.Map<LoginResponseDto>(loginResult);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> RedeemRefreshToken([FromBody] RefreshRequestDto refreshRequestDto)
    {
        var loginResult = await _usersService.RedeemRefreshTokenAsync(refreshRequestDto.RefreshToken);

        return _mapper.Map<LoginResponseDto>(loginResult);
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        await _usersService.LogoutAsync();

        return NoContent();
    }
}
