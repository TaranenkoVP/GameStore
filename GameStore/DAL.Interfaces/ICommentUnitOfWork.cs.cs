using System;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.Interfaces
{
    /// <summary>
    ///     Class <see cref="ICommentUnitOfWork" /> that handles multiple Repositories
    ///     This is not specific to which Data Access
    ///     tools your are using (Direct SQL, EF, NHibernate, etc)
    /// </summary>
    public interface ICommentUnitOfWork : IDisposable
    {
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<Game> GameRepository { get; }

        /// <summary>
        ///     Commit all changes
        /// </summary>
        void Commit();
    }
}