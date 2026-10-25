using CardTrader.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CardTrader.DataAccess;

public static class DbInitializer
{
    public static void Initialize(CardTraderDbContext context)
    {
        // Run migrations
        context.Database.Migrate();

        // Seed sample data
        SeedCards(context);

        // Save changes to the DB
        context.SaveChanges();
    }

    private static void SeedCards(CardTraderDbContext context)
    {
        if (context.Cards.Any())
        {
            return;
        }

        var cards = new List<Card>
        {
            // Common
            new()
            {
                Name = "Gelatinous Cube",
                Rarity = CardRarity.Common,
                Element = CardElement.Water,
                Description = "A transparent cube of jelly that silently sweeps the dungeon corridors clean.",
                Lore = "Nobody knows who first created these quivering cubes, but every dungeon seems to have at least one. "
                    + "They glide through the corridors in perfect silence, so clear that most adventurers only notice them when it is far too late. "
                    + "Anything they touch is slowly pulled inside and dissolved, except for metal and gems, which float within their bodies like flies in amber. "
                    + "Clever explorers have learned to check every hallway for a faint shimmer, and the greedy ones to look inside the cube for a fortune.",
                ImageUrl = "/images/cards/gelatinous-cube.png",
            },
            new()
            {
                Name = "Goomba",
                Rarity = CardRarity.Common,
                Element = CardElement.Earth,
                Description = "Walks straight ahead without a single thought, until someone stomps on it.",
                ImageUrl = "/images/cards/goomba.png",
            },
            new()
            {
                Name = "Creeper",
                Rarity = CardRarity.Common,
                Element = CardElement.Neutral,
                Description = "Silent, green and always right behind you.",
                ImageUrl = "/images/cards/creeper.png",
            },
            new()
            {
                Name = "Goblin",
                Rarity = CardRarity.Common,
                Element = CardElement.Earth,
                Description = "Small, sneaky and always after your gold.",
                ImageUrl = "/images/cards/goblin.png",
            },
            new()
            {
                Name = "Skeleton",
                Rarity = CardRarity.Common,
                Element = CardElement.Dark,
                Description = "Rattles its bones in the depths of forgotten crypts.",
                ImageUrl = "/images/cards/skeleton.png",
            },

            // Uncommon
            new()
            {
                Name = "Charmander",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Fire,
                Description = "The flame on its tail shows how it feels.",
                ImageUrl = "/images/cards/charmander.png",
            },
            new()
            {
                Name = "Olaf",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Water,
                Description = "A cheerful snowman who dreams of summer.",
                ImageUrl = "/images/cards/olaf.png",
            },
            new()
            {
                Name = "Mimic",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Neutral,
                Description = "Looks like a treasure chest. It is not a treasure chest.",
                ImageUrl = "/images/cards/mimic.png",
            },

            // Rare
            new()
            {
                Name = "Geralt of Rivia",
                Rarity = CardRarity.Rare,
                Element = CardElement.Neutral,
                Description = "A monster hunter for hire, with two swords on his back.",
                ImageUrl = "/images/cards/geralt-of-rivia.png",
            },
            new()
            {
                Name = "Sonic",
                Rarity = CardRarity.Rare,
                Element = CardElement.Air,
                Description = "Gone faster than the wind before you can blink.",
                ImageUrl = "/images/cards/sonic.png",
            },
            new()
            {
                Name = "Owlbear",
                Rarity = CardRarity.Rare,
                Element = CardElement.Earth,
                Description = "Half owl, half bear, fully dangerous.",
                ImageUrl = "/images/cards/owlbear.png",
            },
            new()
            {
                Name = "Unicorn",
                Rarity = CardRarity.Rare,
                Element = CardElement.Light,
                Description = "Its horn shines with a pure light that heals any wound.",
                ImageUrl = "/images/cards/unicorn.png",
            },

            // Epic
            new()
            {
                Name = "Jinx",
                Rarity = CardRarity.Epic,
                Element = CardElement.Fire,
                Description = "Loves chaos, loud noises and bigger explosions.",
                ImageUrl = "/images/cards/jinx.png",
            },
            new()
            {
                Name = "Beholder",
                Rarity = CardRarity.Epic,
                Element = CardElement.Dark,
                Description = "A floating eye tyrant whose every gaze casts a spell.",
                ImageUrl = "/images/cards/beholder.png",
            },

            // Legendary
            new()
            {
                Name = "Korra",
                Rarity = CardRarity.Legendary,
                Element = CardElement.Water,
                Description = "The Avatar, master of all four elements.",
                Lore = "Born in the Southern Water Tribe, Korra revealed herself as the Avatar while still a small child, bending water, earth and fire with ease. "
                    + "Air was a different story: it took her years of struggle and the hardest lessons of her life to master the most peaceful of the elements. "
                    + "In Republic City she faced enemies who wanted to take bending away from the world, opened the portals to the Spirit World and stood against a dictator with an army of machines. "
                    + "Every fight left a scar, but Korra learned that being the Avatar means not only strength, but also the courage to heal and to listen.",
                ImageUrl = "/images/cards/korra.png",
            },
            new()
            {
                Name = "Dracula",
                Rarity = CardRarity.Legendary,
                Element = CardElement.Dark,
                Description = "Lord of the vampires, driven by grief and vengeance.",
                Lore = "For centuries Vlad Dracula Tepes lived alone in his castle, until a young woman named Lisa came knocking, asking him to teach her the secrets of science and medicine. "
                    + "Her curiosity and kindness slowly brought him back to humanity, and the two of them fell in love. "
                    + "When the Church burned her at the stake as a witch for healing people, Dracula gave the land a single year to make amends. "
                    + "No one did, and when the year was over, his armies of the night swept across the land in a war meant to erase humanity itself.",
                ImageUrl = "/images/cards/dracula.png",
            },
        };

        context.Cards.AddRange(cards);
    }
}
