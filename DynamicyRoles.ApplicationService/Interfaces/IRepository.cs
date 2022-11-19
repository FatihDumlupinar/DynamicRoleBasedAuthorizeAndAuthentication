using System.Linq.Expressions;

namespace DynamicyRoles.ApplicationService.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAllAsync(List<TEntity> entity);
        Task AddAsync(TEntity entity);
        Task<TEntity> AddAsyncReturnEntity(TEntity entity);
        Task DeleteAllAsync(List<TEntity> entity);
        Task DeleteAsync(TEntity entity);
        TEntity? Get(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter);
        Task UpdateAllAsync(List<TEntity> entity);
        Task UpdateAsync(TEntity entity);


    }
}
