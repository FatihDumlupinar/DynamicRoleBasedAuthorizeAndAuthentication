using DynamicyRoles.ApplicationService.Interfaces;
using DynamicyRoles.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DynamicyRoles.Persistence.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<TEntity>();
        }

        public Task AddAllAsync(List<TEntity> entity)
        {
            return _entities.AddRangeAsync(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            _ = await _entities.AddAsync(entity);
        }

        public async Task<TEntity> AddAsyncReturnEntity(TEntity entity)
        {
            var entities = await _entities.AddAsync(entity);
            return entities.Entity;
        }

        public Task DeleteAllAsync(List<TEntity> entity)
        {
            return Task.Run(() =>
            {
                _entities.RemoveRange(entity);
            });
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                _ = _entities.Remove(entity);
            });
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.FirstOrDefault(filter);
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? _entities : _entities.Where(filter);
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? _entities.ToListAsync() : _entities.Where(filter).ToListAsync();
        }

        public Task UpdateAllAsync(List<TEntity> entity)
        {
            return Task.Run(() =>
            {
                _entities.UpdateRange(entity);
            });
        }

        public Task UpdateAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                _entities.Update(entity);
            });
        }
    }
}
