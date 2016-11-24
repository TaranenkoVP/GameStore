using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Infrastructure.CustomExceptions;
using GameStore.BLL.Interfaces.Services;
using GameStore.BLL.Services.Common;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Models;

namespace GameStore.BLL.Services
{
    public class GameService : EntityService, IGameService
    {
        protected IUnitOfWork Database;

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Genre> _genreRepository;
        private IGenericRepository<PlatformType> _platformTypeRepository;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _gameRepository = uow.GetRepository<Game>();
            _genreRepository = uow.GetRepository<Genre>();
            _platformTypeRepository = uow.GetRepository<PlatformType>();
            MapperBll = mapper;
        }

        public async Task<List<GameBll>> GetAllAsync()
        {
            var games = await GetGamesAsync();
            return MapperBll.Map<List<GameBll>>(games);
        }

        public async Task<GameBll> GetDetailsByKeyAsync(string gamekey)
        {
            if (!IsValidKey(gamekey))
                return null;

            var game = await GetGameByKeyAsync(gamekey, "Genres, PlatformTypes");
            return MapperBll.Map<GameBll>(game);
        }

        public async Task<List<GameBll>> GetByGenreAsync(string name)
        {
            if (!IsValidKey(name))
                return null;

            var games = await GetGamesAsync(d => d.Genres.Any(t => t.Name.Equals(name)));
            return MapperBll.Map<List<GameBll>>(games);
        }

        public async Task<List<GameBll>> GetByPlatformTypeAsync(string platformType)
        {
            if (!IsValidKey(platformType))
                return null;

            var games = await GetGamesAsync(d => d.PlatformTypes.Any(t => t.Type.Equals(platformType)));
            return MapperBll.Map<List<GameBll>>(games);
        }

        public async Task AddAsync(GameBll entity)
        {
            if (!IsValidGame(entity))
                throw new ValidationException($"The game is not valid");

            if (await GetGameByKeyAsync(entity.Key) != null)
                throw new ValidationException($"The game with the key: {entity.Key} is already exist");

            var game = MapperBll.Map<Game>(entity);
            await ProjectGenresToDAlAsync(game.Genres, entity.Genres);
            await ProjectPlatformTypesToDAlAsync(game.PlatformTypes, entity.PlatformTypes);
            try
            {
                _gameRepository.Add(game);
                await Database.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not add game {entity.Key} to database", "", ex);
            }
        }

        public async Task UpdateAsync(GameBll entity)
        {
            if (!IsValidGame(entity))
                return;

            var game = await GetGameByKeyAsync(entity.Key);
            if (game == null)
                throw new ValidationException($"The game {entity.Key} is not exist");

            MapperBll.Map(entity, game);
            await ProjectGenresToDAlAsync(game.Genres, entity.Genres);
            await ProjectPlatformTypesToDAlAsync(game.PlatformTypes, entity.PlatformTypes);
            try
            {
                _gameRepository.Update(game);
                await Database.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not add game {entity.Key} to database", "", ex);
            }
        }

        public async Task DeleteAsync(string gamekey)
        {
            if (!IsValidKey(gamekey))
                throw new ValidationException("Empty game key");
            try
            {
                _gameRepository.Delete(await GetGameByKeyAsync(gamekey));
                await Database.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not get game {gamekey} from database", "", ex);
            }
        }

        public async Task<byte[]> DownloadAsync(string gamekey)
        {
            if (!IsValidKey(gamekey))
                return null;

            var game = await GetGameByKeyAsync(gamekey, "Comments");
            if (game == null)
                throw new ValidationException($"The game {gamekey} not found");
            if (string.IsNullOrEmpty(game.DownloadPath))
                throw new ValidationException($"The game {gamekey} download path is empty");

            return GetFile(game.DownloadPath);
        }

        private bool IsValidKey(string key)
        {
            return !string.IsNullOrEmpty(key);
        }

        private bool IsValidGame(GameBll gameToValidate)
        {
            var errors = new Dictionary<string, string>();
            if (gameToValidate == null)
            {
                errors.Add("", "Empty game");
            }
            else
            {
                if (string.IsNullOrEmpty(gameToValidate.Key))
                    errors.Add("Key", "Key is required.");
                if (string.IsNullOrEmpty(gameToValidate.Name))
                    errors.Add("Name", "Name is required.");
            }
            if (errors.Count != 0)
                throw new ValidationException("The game is not valid");

            return true;
        }

        private async Task<Game> GetGameByKeyAsync(string gamekey, string includeProperties = "")
        {
            if (gamekey == null)
                throw new ValidationException("Empty game key");

            IEnumerable<Game> games = null;
            try
            {
                games = await _gameRepository.GetAsync(
                    x => x.Key.Equals(gamekey),
                    includeProperties: includeProperties);
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not get game {gamekey} from database", "", ex);
            }

            return games.FirstOrDefault();
        }

        private async Task<List<Game>> GetGamesAsync(
            Expression<Func<Game, bool>> filter = null,
            Func<IQueryable<Game>, IOrderedQueryable<Game>> orderBy = null,
            string includeProperties = "")
        {
            IEnumerable<Game> games = null;
            try
            {
                games = await _gameRepository.GetAsync(filter, orderBy, includeProperties);
            }
            catch (Exception ex)
            {
                throw new AccessException("Can not get games from database", "", ex);
            }

            return games.ToList();
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

        private async Task ProjectGenresToDAlAsync(ICollection<Genre> genreDal, IEnumerable<GenreBll> genreBll)
        {
            genreDal.Clear();
            //TODO better SQL query
            foreach (var item in genreBll)
            {
                Genre genre = null;
                try
                {
                    genre = await _genreRepository.FindByIdAsync(item.Id);
                }
                catch (Exception ex)
                {
                    throw new AccessException($"Can not get platform type {item.Name} from database", "", ex);
                }

                if (genre == null)
                    throw new ValidationException($"The genre {item.Name} is not exist");

                genreDal.Add(genre);
            }
        }

        private async Task ProjectPlatformTypesToDAlAsync(ICollection<PlatformType> platformTypeDal,
            IEnumerable<PlatformTypeBll> platformTypeBll)
        {
            platformTypeDal.Clear();
            //TODO better SQL query
            foreach (var item in platformTypeBll)
            {
                PlatformType platformType = null;
                try
                {
                    platformType = await _platformTypeRepository.FindByIdAsync(item.Id);
                }
                catch (Exception ex)
                {
                    throw new AccessException($"Can not get platform type {item.Type} from database", "", ex);
                }

                if (platformType == null)
                    throw new ValidationException($"The platform type {item.Type} is not exist");

                platformTypeDal.Add(platformType);
            }
        }
    }
}