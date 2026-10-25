using Mapster;

namespace CardTrader.WebApi.Infrastructure;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Mapping rules
        // config.NewConfig<Card, CardResponseDto>();
    }
}
