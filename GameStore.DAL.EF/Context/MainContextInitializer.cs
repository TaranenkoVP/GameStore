using System.Data.Entity;
using GameStore.DAL.Models;

namespace GameStore.DAL.EF.Context
{
    public class MainContextInitializer : DropCreateDatabaseIfModelChanges<MainContext>
    {
        protected override void Seed(MainContext context)
        {
            var platforms = new[]
            {
                new PlatformType {Type = "Mobile"},
                new PlatformType {Type = "Browser"},
                new PlatformType {Type = "Desktop"},
                new PlatformType {Type = "Console"}
            };

            var genres = new[]
            {
                new Genre {Name = "Strategy"},
                new Genre {Name = "RPG"},
                new Genre {Name = "Sports"},
                new Genre {Name = "Races"},
                new Genre {Name = "Action"},
                new Genre {Name = "Adventure"},
                new Genre {Name = "Puzzle&Skill"},
                new Genre {Name = "Misc"}
            };

            var strategySubgenres = new[]
            {
                new Genre {Name = "RTS"},
                new Genre {Name = "TBS"}
            };

            var raceSubgenres = new[]
            {
                new Genre {Name = "Rally"},
                new Genre {Name = "Arcade"},
                new Genre {Name = "Formula"},
                new Genre {Name = "Off-road"}
            };

            var actionSubgenres = new[]
            {
                new Genre {Name = "FPS"},
                new Genre {Name = "TPS"}
            };

            genres[0].SubGenres = strategySubgenres;
            genres[3].SubGenres = raceSubgenres;
            genres[4].SubGenres = actionSubgenres;

            var games = new[]
            {
                new Game
                {
                    Key = "WOW",
                    Name = "World of Warcraft",
                    Description = "",
                    Genres = new[] {genres[2]},
                    PlatformTypes = new[] {platforms[1]}
                },
                new Game
                {
                    Key = "NFS",
                    Name = "Need for speed",
                    Description = "The best races ever",
                    Genres = new[] {genres[3], raceSubgenres[1]},
                    PlatformTypes = new[] {platforms[0], platforms[2]}
                },
                new Game
                {
                    Key = "FIFA",
                    Name = "FIFA 2017",
                    Description = "Football simulator",
                    Genres = new[] {genres[2]},
                    PlatformTypes = new[] {platforms[1], platforms[3]}
                },
                new Game
                {
                    Key = "Civ",
                    Name = "Civilization V",
                    Description = "The new game",
                    Genres = new[] {genres[0], strategySubgenres[1]},
                    PlatformTypes = new[] {platforms[2], platforms[3]}
                }
            };

            var comment1 = new Comment
            {
                Name = "Vasiliy",
                Game = games[0],
                Body = "The game broke my life!"
            };

            var subcomment1 = new Comment
            {
                Name = "Alex",
                Game = games[0],
                Body = "It's just because you're playing a warlock",
                Author = comment1
            };

            foreach (var platform in platforms)
                context.PlatformType.Add(platform);

            foreach (var genre in genres)
                context.Genre.Add(genre);

            foreach (var game in games)
                context.Game.Add(game);

            context.Comment.Add(comment1);
            context.Comment.Add(subcomment1);

            context.SaveChanges();
        }
    }
}