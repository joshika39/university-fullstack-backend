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
                ImageUrl = "gelatinous-cube.webp",
            },
            new()
            {
                Name = "Goomba",
                Rarity = CardRarity.Common,
                Element = CardElement.Earth,
                Description = "Walks straight ahead without a single thought, until someone stomps on it.",
                ImageUrl = "goomba.webp",
            },
            new()
            {
                Name = "Creeper",
                Rarity = CardRarity.Common,
                Element = CardElement.Neutral,
                Description = "Silent, green and always right behind you.",
                ImageUrl = "creeper.webp",
            },
            new()
            {
                Name = "Goblin",
                Rarity = CardRarity.Common,
                Element = CardElement.Earth,
                Description = "Small, sneaky and always after your gold.",
                ImageUrl = "goblin.webp",
            },
            new()
            {
                Name = "Skeleton",
                Rarity = CardRarity.Common,
                Element = CardElement.Dark,
                Description = "Rattles its bones in the depths of forgotten crypts.",
                ImageUrl = "skeleton.webp",
            },

            // Uncommon
            new()
            {
                Name = "Charmander",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Fire,
                Description = "The flame on its tail shows how it feels.",
                ImageUrl = "charmander.webp",
            },
            new()
            {
                Name = "Olaf",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Water,
                Description = "A cheerful snowman who dreams of summer.",
                ImageUrl = "olaf.webp",
            },
            new()
            {
                Name = "Mimic",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Neutral,
                Description = "Looks like a treasure chest. It is not a treasure chest.",
                ImageUrl = "mimic.webp",
            },

            // Rare
            new()
            {
                Name = "Geralt of Rivia",
                Rarity = CardRarity.Rare,
                Element = CardElement.Neutral,
                Description = "A monster hunter for hire, with two swords on his back.",
                ImageUrl = "geralt-of-rivia.webp",
            },
            new()
            {
                Name = "Sonic",
                Rarity = CardRarity.Rare,
                Element = CardElement.Air,
                Description = "Gone faster than the wind before you can blink.",
                ImageUrl = "sonic.webp",
            },
            new()
            {
                Name = "Owlbear",
                Rarity = CardRarity.Rare,
                Element = CardElement.Earth,
                Description = "Half owl, half bear, fully dangerous.",
                ImageUrl = "owlbear.webp",
            },
            new()
            {
                Name = "Unicorn",
                Rarity = CardRarity.Rare,
                Element = CardElement.Light,
                Description = "Its horn shines with a pure light that heals any wound.",
                ImageUrl = "unicorn.webp",
            },

            // Epic
            new()
            {
                Name = "Jinx",
                Rarity = CardRarity.Epic,
                Element = CardElement.Fire,
                Description = "Loves chaos, loud noises and bigger explosions.",
                ImageUrl = "jinx.webp",
            },
            new()
            {
                Name = "Beholder",
                Rarity = CardRarity.Epic,
                Element = CardElement.Dark,
                Description = "A floating eye tyrant whose every gaze casts a spell.",
                ImageUrl = "beholder.webp",
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
                ImageUrl = "korra.webp",
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
                ImageUrl = "dracula.webp",
            },

            // UI checks
            new()
            {
                Name = "The Unknown",
                Rarity = CardRarity.Common,
                Element = CardElement.Neutral,
                Description = "It exists. Probably. The asset server isn't so sure.",
                Lore = "Scholars argue whether it was never drawn or simply lost in a migration. "
                    + "Some claim to have glimpsed it in the fallback placeholder, a grey silhouette staring back from the void. "
                    + "Those who look too long start seeing alt text everywhere.",
                ImageUrl = "the-unknown.webp", // Deliberately missing file to test the image fallback
            },
            new()
            {
                Name = "The Flattened",
                Rarity = CardRarity.Uncommon,
                Element = CardElement.Earth,
                Description = "Born 3:4, raised 32:9. Bends grids and breaks UIs.",
                Lore = "Legend says a careless designer once dragged its corner without holding Shift. "
                    + "It has never been the same since, and neither have the layouts it touches. "
                    + "Frontend developers whisper its name when cards overflow their containers.",
                ImageUrl = "the-flattened.webp", // Deliberately wide image (~32:9) to test aspect ratio handling
            },
            new()
            {
                Name = "The Translucent",
                Rarity = CardRarity.Epic,
                Element = CardElement.Light,
                Description = "Wears an alpha channel and nothing else. Haunts dark mode, vanishes on white.",
                Lore = "It drifts between layers, borrowing its color from whatever lies beneath. "
                    + "Against the night it glows like a lantern, but when the lights come on, it is nowhere to be seen. "
                    + "Only those who set a checkerboard background have ever seen its true form.",
                ImageUrl = "the-translucent.webp", // Semi-transparent image to test alpha channel handling
            },
        };

        context.Cards.AddRange(cards);
    }
}
