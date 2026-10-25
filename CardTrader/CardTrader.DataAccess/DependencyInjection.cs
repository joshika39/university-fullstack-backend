using CardTrader.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardTrader.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
    {
        // Database
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<CardTraderDbContext>(options => options
            .UseNpgsql(connectionString)
        );

        // Services
        services.AddScoped<ICardService, CardService>();

        return services;
    }
}
