namespace CardTrader.Shared.Dtos;

public record LoginResponseDto
{
    public required string UserId { get; init; }
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
