using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CommentService : EntityService, ICommentService
    {
        protected IUnitOfWork Database;

        private IGenericRepository<Comment> _commentRepository;

        public CommentService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _commentRepository = uow.GetRepository<Comment>();
            MapperBll = mapper;
        }

        public async Task<List<CommentBll>> GetCommentsAsync(string gamekey)
        {
            if (gamekey == null)
                throw new ValidationException("Invalid gamekey");

            IEnumerable<Comment> comments = null;
            try
            {
                comments = await _commentRepository.GetAsync(x => x.Game.Key.Equals(gamekey));
            }
            catch (Exception ex)
            {
                throw new AccessException("Can't get the game from database", "", ex);
            }
            if (comments == null)
                throw new ValidationException("The Game not found");

            return MapperBll.Map<List<CommentBll>>(comments);
        }

        public async Task AddAsync(CommentBll entity)
        {
            if (!IsValidComment(entity))
                return;

            var comment = MapperBll.Map<Comment>(entity);
            try
            {
                _commentRepository.Add(comment);
                await Database.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not add comment {entity.Name} to database", "", ex);
            }
        }

        public async Task UpdateAsync(CommentBll entity)
        {
            if (!IsValidComment(entity))
                return;

            var comment = MapperBll.Map<Comment>(entity);
            try
            {
                _commentRepository.Add(comment);
                await Database.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not add comment {entity.Name} to database", "", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id == 0)
                throw new ValidationException("The comment id should be not null");
            try
            {
                _commentRepository.Delete(await GetCommentByIdAsync(id));
                await Database.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not get game {id} from database", "", ex);
            }
        }

        private bool IsValidComment(CommentBll gameToValidate)
        {
            var errors = new Dictionary<string, string>();
            if (gameToValidate == null)
            {
                errors.Add("", "Empty game");
            }
            else
            {
                if (gameToValidate.GameId == 0)
                    errors.Add("Name", "GameId is required.");
                if (string.IsNullOrEmpty(gameToValidate.Name))
                    errors.Add("Name", "Name is required.");
            }
            if (errors.Count != 0)
                throw new ValidationException("The game is not valid");

            return true;
        }

        private async Task<Comment> GetCommentByIdAsync(int id, string includeProperties = "")
        {
            if (id == 0)
                throw new ValidationException("The comment id should be not null");

            IEnumerable<Comment> comments = null;
            try
            {
                comments = await _commentRepository.GetAsync(
                    x => x.Id == id,
                    includeProperties: includeProperties);
            }
            catch (Exception ex)
            {
                throw new AccessException($"Can not get game with id: {id} from database", "", ex);
            }

            return comments.FirstOrDefault();
        }

        private bool IsValidKey(string key)
        {
            return !string.IsNullOrEmpty(key);
        }
    }
}