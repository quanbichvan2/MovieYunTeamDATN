using Microsoft.EntityFrameworkCore;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Shared.Abstractions.Repositories
{
    public interface IBaseUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
        void Entry<TEntity>(TEntity entity, EntityState state) where TEntity : class, IEntity;
    }
}