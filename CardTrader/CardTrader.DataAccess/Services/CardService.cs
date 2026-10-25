using CardTrader.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CardTrader.DataAccess.Services;

internal class CardService : ICardService
{
    private readonly CardTraderDbContext _context;

    public CardService(CardTraderDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Card>> GetAllAsync()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task<Card> GetByIdAsync(int id)
    {
        var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        if (card is null)
            throw new KeyNotFoundException($"Card with id {id} not found");

        return card;
    }
}
