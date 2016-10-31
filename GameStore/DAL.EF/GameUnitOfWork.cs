using System;
using DAL.EF.Infrastructure;
using DAL.EF.Repositories;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.EF
{
    public class GameUnitOfWork : IGameUnitOfWork
    {
        #region .ctor

        /// <summary>
        ///     The class constructor
        /// </summary>
        public GameUnitOfWork()
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
        private IGenericRepository<Genre> _genreRepository;
        private IGenericRepository<PlatformType> _platformTypeRepository;

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

        public IGenericRepository<Genre> GenreRepository
        {
            get
            {
                return _genreRepository ??
                       (_genreRepository = new GenericRepository<Genre>(_context));
            }
        }

        public IGenericRepository<PlatformType> PlatformTypeRepository
        {
            get
            {
                return _platformTypeRepository ??
                       (_platformTypeRepository = new GenericRepository<PlatformType>(_context));
            }
        }

        #endregion
    }
}