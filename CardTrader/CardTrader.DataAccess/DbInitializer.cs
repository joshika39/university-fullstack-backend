using Microsoft.EntityFrameworkCore;

namespace CardTrader.DataAccess;

public static class DbInitializer
{
    public static void Initialize(CardTraderDbContext context)
    {
        // Run migrations
        context.Database.Migrate();

        // Seed sample data
        // SeedCards(context);

        // Save changes to the DB
        context.SaveChanges();
    }
}
