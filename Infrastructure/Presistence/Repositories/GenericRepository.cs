using Domain.Contracts;
using Domain.Entites;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) 
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTraking = false)
            => asNoTraking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
            : await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);


        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);


        #region Specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
        #endregion
    }
}
