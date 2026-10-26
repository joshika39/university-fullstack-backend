using CardTrader.DataAccess.Services;
using CardTrader.Shared.Dtos;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace CardTrader.WebApi.Controllers;

[Route("api/cards")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICardService _cardService;

    public CardsController(IMapper mapper, ICardService cardService)
    {
        _mapper = mapper;
        _cardService = cardService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CardResponseDto>>> GetCards()
    {
        var cards = await _cardService.GetAllAsync();
        var cardResponseDtos = _mapper.Map<List<CardResponseDto>>(cards);

        return cardResponseDtos;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CardResponseDto>> GetCardById([FromRoute] int id)
    {
        try
        {
            var card = await _cardService.GetByIdAsync(id);
            var cardResponseDto = _mapper.Map<CardResponseDto>(card);

            return cardResponseDto;
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
