using System;
using System.Collections.Generic;
using Bll.Core.Interfaces;
using BLL.Core.Entities;
using BLL.Infrastructure.CustomExceptions;
using BLL.Services.Common;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace BLL.Services
{
    public class CommentService : EntityService, ICommentService
    {
        //protected ICommentUnitOfWork Database;

        //public CommentService(ICommentUnitOfWork uow)
        //{
        //    Database = uow;
        //}
        protected IGenericRepository<Comment> CommentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            CommentRepository = commentRepository;
        }


        public IEnumerable<CommentBll> GetComments(string gamekey)
        {
            if (gamekey == null)
                throw new ValidationException("Invalid gamekey", "");

            IEnumerable<Comment> comments = null;
            try
            {
                comments = CommentRepository.Get(x => x.Game.Key.Equals(gamekey));
            }
            catch (Exception ex)
            {
                throw new AccessException("Can't get the game from database", "", ex);
            }
            if (comments == null)
                throw new ValidationException("The Game not found", "");

            return MapperBll.Map<List<CommentBll>>(comments);
        }

        public void Add(CommentBll entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CommentBll entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }
    }
}