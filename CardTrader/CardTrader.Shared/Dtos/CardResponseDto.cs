namespace CardTrader.Shared.Dtos;

public record CardResponseDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required CardRarityDto Rarity { get; init; }
    public required CardElementDto Element { get; init; }
    public required string Description { get; init; }
    public string? Lore { get; init; }
    public required string ImageUrl { get; init; }
}
