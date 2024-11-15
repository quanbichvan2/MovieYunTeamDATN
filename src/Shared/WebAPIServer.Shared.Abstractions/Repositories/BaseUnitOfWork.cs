using Microsoft.EntityFrameworkCore;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Shared.Abstractions.Repositories
{
    public abstract class BaseUnitOfWork<TDbContext>: IBaseUnitOfWork where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        public BaseUnitOfWork(TDbContext context)
        {
            _context = context;
        }
        public void Entry<TEntity>(TEntity entity, EntityState state) where TEntity : class, IEntity
        {
            var entry = _context.Entry(entity);
            entry.State = state;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
