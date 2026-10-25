using System.ComponentModel.DataAnnotations;

namespace CardTrader.DataAccess.Models;

public class Card
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public CardRarity Rarity { get; set; }

    public CardElement Element { get; set; }

    [MaxLength(200)]
    public string Description { get; set; } = null!;

    [MaxLength(2000)]
    public string? Lore { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; } = null!;
}
