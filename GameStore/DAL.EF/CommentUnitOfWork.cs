using System;
using DAL.EF.Infrastructure;
using DAL.EF.Repositories;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.EF
{
    public class CommentUnitOfWork : ICommentUnitOfWork
    {
        #region .ctor

        /// <summary>
        ///     The class constructor
        /// </summary>
        public CommentUnitOfWork()
        {
            _context = new MainContext();
        }

        #endregion

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        #region Fields

        private readonly MainContext _context;

        private bool _disposed;

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Comment> _commentRepository;

        #endregion

        #region Properties

        public IGenericRepository<Game> GameRepository
        {
            get
            {
                return _gameRepository ??
                       (_gameRepository = new GenericRepository<Game>(_context));
            }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {
                return _commentRepository ??
                       (_commentRepository = new GenericRepository<Comment>(_context));
            }
        }

        #endregion
    }
}