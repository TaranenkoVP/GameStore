using System;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.Interfaces
{
    /// <summary>
    ///     Class <see cref="IGameUnitOfWork" /> that handles multiple Repositories
    ///     This is not specific to which Data Access
    ///     tools your are using (Direct SQL, EF, NHibernate, etc)
    /// </summary>
    public interface IGameUnitOfWork : IDisposable
    {
        IGenericRepository<Game> GameRepository { get; }
        //IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }
        IGenericRepository<PlatformType> PlatformTypeRepository { get; }

        /// <summary>
        ///     Commit all changes
        /// </summary>
        void Commit();
    }
}