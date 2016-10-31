using System.Data.Entity;
using DAL.Models;

namespace DAL.EF.Infrastructure
{
    public class MainContextInitializer : DropCreateDatabaseIfModelChanges<MainContext>
    {
        protected override void Seed(MainContext context)
        {
            var p1 = new PlatformType {Type = "mobile"};
            var p2 = new PlatformType {Type = "browser"};
            context.PlatformType.Add(p1);
            context.PlatformType.Add(p2);
            context.SaveChanges();

            var genre1 = new Genre {Name = "Strategy"};
            var genre2 = new Genre {Name = "RPG"};
            context.Genre.Add(genre1);
            context.Genre.Add(genre2);
            context.SaveChanges();

            var genre3 = new Genre {Name = "RTS", ParentGenre = genre1};
            var genre4 = new Genre {Name = "TBS", ParentGenre = genre1};
            context.Genre.Add(genre3);
            context.Genre.Add(genre4);
            context.SaveChanges();

            var g1 = new Game
            {
                Key = "WOW",
                Name = "World of Warcraft",
                Description = "The good game",
                Genres = new[] {genre1, genre2, genre3, genre4},
                PlatformTypes = new[] {p1, p2}
            };
            var g2 = new Game {Key = "Civ", Name = "Civilization", Description = "The new game"};
            var g3 = new Game
            {
                Key = "NFS",
                Name = "Need for speed",
                Genres = new[] {genre1, genre3},
                PlatformTypes = new[] {p1}
            };

            context.Game.Add(g1);
            context.Game.Add(g2);
            context.Game.Add(g3);

            context.SaveChanges();
        }
    }
}