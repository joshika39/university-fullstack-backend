namespace CardTrader.Shared.Dtos;

public record RefreshRequestDto
{
    public required string RefreshToken { get; init; }
}
