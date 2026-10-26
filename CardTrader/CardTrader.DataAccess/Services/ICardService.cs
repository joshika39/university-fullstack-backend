using CardTrader.DataAccess.Models;

namespace CardTrader.DataAccess.Services;

public interface ICardService
{
    Task<IReadOnlyCollection<Card>> GetAllAsync();
    Task<Card> GetByIdAsync(int id);
}
