using System.ComponentModel.DataAnnotations;

namespace CardTrader.Shared.Dtos;

public record UserRequestDto
{
    [StringLength(256, ErrorMessage = "Username is too long")]
    public required string UserName { get; init; }

    [EmailAddress(ErrorMessage = "Email format is invalid")]
    public required string Email { get; init; }

    public required string Password { get; init; }
}
