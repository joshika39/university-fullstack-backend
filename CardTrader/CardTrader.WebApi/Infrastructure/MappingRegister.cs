using CardTrader.DataAccess.Models;
using CardTrader.Shared.Dtos;
using Mapster;

namespace CardTrader.WebApi.Infrastructure;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Mapping rules
        config.NewConfig<Card, CardResponseDto>()
            .Map(dest => dest.ImageUrl, src => $"/images/cards/{src.ImageUrl}");
    }
}
