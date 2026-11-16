using CardTrader.DataAccess.Models;
using CardTrader.DataAccess.Services;
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

        config.NewConfig<User, UserResponseDto>();

        config.NewConfig<UserRequestDto, User>()
            .IgnoreNonMapped(true)
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Email, src => src.Email);

        config.NewConfig<LoginResult, LoginResponseDto>();
    }
}
