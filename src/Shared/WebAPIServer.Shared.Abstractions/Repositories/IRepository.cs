using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Shared.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid? id);
		Task<TEntity?> FindByIdAsync(Guid? id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll();
        Task<bool> CreateAsync(TEntity entity);
        bool Update(TEntity entity);
        bool Create(TEntity entity);
        bool Delete(TEntity entity);
    }
}