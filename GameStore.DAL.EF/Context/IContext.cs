using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using GameStore.DAL.Models;

namespace GameStore.DAL.EF.Context
{
    public interface IContext : IDisposable
    {
        IDbSet<Game> Game { get; set; }
        IDbSet<Comment> Comment { get; set; }
        IDbSet<Genre> Genre { get; set; }
        IDbSet<PlatformType> PlatformType { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}