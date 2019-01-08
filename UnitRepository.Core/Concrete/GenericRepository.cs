using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UnitRepository.Core.Abstarct;

namespace UnitRepository.Core.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region Implementation of the IGenericRepository<T> interface

        public IQueryable<TEntity> GetAll => _dbContext.Set<TEntity>();

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetByID(object id) => _dbContext.Set<TEntity>().Find(id);

        public void Insert(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = _dbContext.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        #endregion
    }
}
