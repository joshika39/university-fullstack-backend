using CardTrader.DataAccess.Services;
using CardTrader.Shared.Dtos;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardTrader.WebApi.Controllers;

[Authorize]
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public UsersController(IMapper mapper, IUsersService usersService)
    {
        _mapper = mapper;
        _usersService = usersService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResponseDto>> GetUserById([FromRoute] string id)
    {
        var user = await _usersService.GetUserByIdAsync(id);
        var userResponseDto = _mapper.Map<UserResponseDto>(user);

        return userResponseDto;
    }
}
