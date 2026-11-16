using System.ComponentModel.DataAnnotations;

namespace CardTrader.Shared.Dtos;

public record LoginRequestDto
{
    [EmailAddress(ErrorMessage = "Email format is invalid")]
    public required string Email { get; init; }

    public required string Password { get; init; }
}
