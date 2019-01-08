using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitRepository.Core.Abstarct
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IGenericRepository<TEntity> CreateRepositoryInstance<TEntity>(TDbContext dbContext) where TEntity : class;
    }
}
