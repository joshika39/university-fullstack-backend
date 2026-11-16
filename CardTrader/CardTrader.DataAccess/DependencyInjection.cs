using CardTrader.DataAccess.Models;
using CardTrader.DataAccess.Services;
using Microsoft.AspNetCore.Identity;
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

        // Identity
        services.AddIdentity<User, IdentityRole>(options =>
        {
            // Password
            options.Password.RequiredLength = 6; // Default

            // Lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Default
            options.Lockout.MaxFailedAccessAttempts = 5; // Default

            // User
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<CardTraderDbContext>()
        .AddDefaultTokenProviders();

        // Services
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}
