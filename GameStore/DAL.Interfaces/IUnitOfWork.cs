using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.Interfaces
{
    /// <summary>
    ///     Class <see cref="IUnitOfWork" /> that handles multiple Repositories
    ///     This is not specific to which Data Access
    ///     tools your are using (Direct SQL, EF, NHibernate, etc)
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Game> GameRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }
        IGenericRepository<PlatformType> PlatformTypeRepository { get; }

        /// <summary>
        ///     Commit all changes
        /// </summary>
        void Commit();
    }
}
