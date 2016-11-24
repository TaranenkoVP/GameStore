using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameStore.DAL.EF.Context;
using GameStore.DAL.Interfaces.Repositories;

namespace GameStore.DAL.EF.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region .ctor

        public GenericRepository(IContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        #endregion .ctor

        #region Fields

        protected IContext Context;
        protected DbSet<TEntity> DbSet;

        #endregion Fields

        #region Methods

        #region Search Functionality

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int count = 0)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (count > 0)
                query = query.Take(count);

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters).ToList();
        }

        #endregion

        #region CRUD Functionality

        public virtual void Add(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;
            else
                DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(entity);

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
                Delete(entity);
        }

        public virtual void Detach(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);

            entry.State = EntityState.Detached;
        }
       
        #endregion
        #endregion
    }
}