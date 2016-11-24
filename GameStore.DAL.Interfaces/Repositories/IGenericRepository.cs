using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces.Repositories
{
    /// <summary>
    ///     Repository for CRUD Operations and methods
    ///     to locate entities within your store. This is not specific to which Data Access
    ///     tools your are using (Direct SQL, EF, etc).
    /// </summary>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Get all entities from db
        /// </summary>
        /// <returns>An IEnumerable object.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        ///     Allow the calling code to specify a filter condition
        ///     and a column to order the results by, and a string parameter
        /// </summary>
        /// <param name="filter">Filter expression for the return Entities</param>
        /// <param name="orderBy">Represents the order of the return Entities</param>
        /// <param name="includeProperties">Include Properties for the navigation properties</param>
        /// <param name="count">Filter to take first Entities(take all by default). </param>
        /// <returns>An IEnumerable object.</returns>
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int count = 0);

        Task<TEntity> FindByIdAsync(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        void Detach(TEntity entity);
    }
}