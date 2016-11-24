using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DAL.EF.Context;
using GameStore.DAL.EF.Repositories;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;

namespace GameStore.DAL.EF
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, IContext, new()
    {
        private readonly IContext _context;
        private bool _disposed;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(IContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _disposed = false;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)))
                return _repositories[typeof(T)] as IGenericRepository<T>;

            var repository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), repository);

            return repository;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                _context.Dispose();
            _disposed = true;
        }
    }
}