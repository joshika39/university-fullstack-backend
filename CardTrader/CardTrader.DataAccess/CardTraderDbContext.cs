using CardTrader.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardTrader.DataAccess;

public class CardTraderDbContext : IdentityDbContext<User>
{
    // DB tables
    public DbSet<Card> Cards { get; set; } = null!;

    public CardTraderDbContext(DbContextOptions<CardTraderDbContext> options)
        : base(options)
    {
    }
}
