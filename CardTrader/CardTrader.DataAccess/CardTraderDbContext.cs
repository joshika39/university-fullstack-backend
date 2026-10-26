using CardTrader.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CardTrader.DataAccess;

public class CardTraderDbContext : DbContext
{
    // DB tables
    public DbSet<Card> Cards { get; set; } = null!;

    public CardTraderDbContext(DbContextOptions<CardTraderDbContext> options)
        : base(options)
    {
    }
}
