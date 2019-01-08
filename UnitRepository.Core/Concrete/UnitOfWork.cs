using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitRepository.Core.Abstarct;

namespace UnitRepository.Core.Concrete
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        #region Implement Interface of the IUnitOfWork

        public IGenericRepository<TEntity> CreateRepositoryInstance<TEntity>(TDbContext dbContext) where TEntity : class
        {
            GenericRepository<TEntity> repoInstance = null;
            try
            {
                repoInstance = new GenericRepository<TEntity>(dbContext);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return repoInstance;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            IGenericRepository<TEntity> repoInstance = null;
            try
            {
                if (_repositories.ContainsKey(typeof(TEntity)))
                {
                    return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
                }
                else
                {
                    repoInstance = CreateRepositoryInstance<TEntity>(_dbContext);
                    _repositories.Add(typeof(TEntity), repoInstance);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return repoInstance;
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
