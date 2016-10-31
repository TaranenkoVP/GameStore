using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Bll.Core.Entities;
using Bll.Core.Entities.GameBll;
using Bll.Core.Interfaces;
using BLL.Core.Entities;
using BLL.Core.Entities.GameBll;
using BLL.Infrastructure.CustomExceptions;
using BLL.Services.Common;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class GameService : EntityService, IGameService
    {
        protected IGameUnitOfWork Database;

        public GameService(IGameUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<GameBll> GetAll()
        {
            var games = GetGames();
            return MapperBll.Map<List<GameBll>>(games);
        }

        public GameDetailsBll GetDetailsByKey(string gamekey)
        {
            var game = GetGameByKey(gamekey, "Genres, PlatformTypes");
            return MapperBll.Map<GameDetailsBll>(game);
        }

        public IEnumerable<CommentBll> GetComments(string gamekey)
        {
            var game = GetGameByKey(gamekey, "Comments");
            if (game == null)
                throw new ValidationException($"The game {gamekey} not found", "");

            return MapperBll.Map<GameWithCommentsBll>(game).Comments;
        }

        public IEnumerable<GameBll> GetByGenre(string name)
        {
            if (name == null)
                throw new ValidationException("Empty genre name", "");

            var games = GetGames(d => d.Genres.Any(t => t.Name.Equals(name)));
            return MapperBll.Map<List<GameBll>>(games);
        }

        public IEnumerable<GameBll> GetByPlatformType(string platformType)
        {
            if (platformType == null)
                throw new ValidationException("Empty platform type", "");

            var games = GetGames(d => d.PlatformTypes.Any(t => t.Type.Equals(platformType)));
            return MapperBll.Map<List<GameBll>>(games);
        }

        public byte[] Download(string gamekey)
        {
            var game = GetGameByKey(gamekey, "Comments");
            if (game == null)
                throw new ValidationException($"The game {gamekey} not found", "");

            var gameBll = MapperBll.Map<GameDetailsBll>(game);
            if (gameBll.DownloadPath == null)
                throw new ValidationException($"The game {gamekey} download path is empty", "");

            return GetFile(gameBll.DownloadPath);
        }

        public void Add(GameDetailsBll entity)
        {
            if (entity == null)
                throw new ValidationException("The game not found", "");

            if (GetGameByKey(entity.Key) != null)
                throw new ValidationException($"The game {entity.Key} is already exist", "");

            var game = MapperBll.Map<Game>(entity);
            ProjectGenresToDAl(game.Genres, entity.Genres);
            ProjectPlatformTypesToDAl(game.PlatformTypes, entity.PlatformTypes);
            try
            {
                Database.GameRepository.Add(game);
                Database.Commit();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not add game {entity.Key} to database", "", ex);
            }
        }

        public void Update(GameDetailsBll entity)
        {
            if (entity == null)
                throw new ValidationException("The game not found", "");

            var game = GetGameByKey(entity.Key);
            if (game == null)
                throw new ValidationException($"The game {entity.Key} is not exist", "");

            MapperBll.Map(entity, game);
            ProjectGenresToDAl(game.Genres, entity.Genres);
            ProjectPlatformTypesToDAl(game.PlatformTypes, entity.PlatformTypes);
            try
            {
                Database.GameRepository.Update(game);
                Database.Commit();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not add game {entity.Key} to database", "", ex);
            }
        }

        public void Delete(string gamekey)
        {
            if (gamekey == null)
                throw new ValidationException("Empty game key", "");
            try
            {
                Database.GameRepository.Delete(GetGameByKey(gamekey));
                Database.Commit();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not get game {gamekey} from database", "", ex);
            }
        }

        private Game GetGameByKey(string gamekey, string includeProperties = "")
        {
            if (gamekey == null)
                throw new ValidationException("Empty game key", "");

            Game game = null;
            try
            {
                game = Database.GameRepository.Get(
                    includeProperties: includeProperties)
                    .FirstOrDefault(x => x.Key.Equals(gamekey));
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not get game {gamekey} from database", "", ex);
            }

            return game;
        }

        private IEnumerable<Game> GetGames(
            Expression<Func<Game, bool>> filter = null,
            Func<IQueryable<Game>, IOrderedQueryable<Game>> orderBy = null,
            string includeProperties = "")
        {
            IEnumerable<Game> games = null;
            try
            {
                games = Database.GameRepository.Get(filter, orderBy, includeProperties);
            }
            catch (Exception ex)
            {
                throw new AccessException("Can not get games from database", "", ex);
            }

            return games;
        }

        private byte[] GetFile(string path)
        {
            var fs = File.OpenRead(path);
            var data = new byte[fs.Length];
            var br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new AccessException("Can not download game from database", "", new IOException(path));
            return data;
        }

        private void ProjectGenresToDAl(ICollection<Genre> genreDal, IEnumerable<GenreBll> genreBll)
        {
            genreDal.Clear();
            //TODO better SQL query
            foreach (var item in genreBll)
            {
                Genre genre = null;
                try
                {
                    genre = Database.GenreRepository.FindById(item.Id);
                }
                catch (Exception ex)
                {
                    throw new AccessException($"Can not get platform type {item.Name} from database", "", ex);
                }

                if (genre == null)
                    throw new ValidationException($"The genre {item.Name} is not exist", "");

                genreDal.Add(genre);
            }
        }

        private void ProjectPlatformTypesToDAl(ICollection<PlatformType> platformTypeDal,
            IEnumerable<PlatformTypeBll> platformTypeBll)
        {
            platformTypeDal.Clear();
            //TODO better SQL query
            foreach (var item in platformTypeBll)
            {
                PlatformType platformType = null;
                try
                {
                    platformType = Database.PlatformTypeRepository.FindById(item.Id);
                }
                catch (Exception ex)
                {
                    throw new AccessException($"Can not get platform type {item.Type} from database", "", ex);
                }

                if (platformType == null)
                    throw new ValidationException($"The platform type {item.Type} is not exist", "");

                platformTypeDal.Add(platformType);
            }
        }
    }
}