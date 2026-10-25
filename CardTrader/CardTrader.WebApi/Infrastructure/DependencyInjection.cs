using Mapster;
using MapsterMapper;
using System.Reflection;

namespace CardTrader.WebApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Require a matching source member for every destination member
        config.Default.RequireDestinationMemberSource(true);

        // Collect the mapping rules from the IRegister implementations of the assembly
        config.Scan(Assembly.GetExecutingAssembly());

        // Validate mapping config
        config.Compile();

        // Add mapping config
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
