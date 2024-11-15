using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsNameExistsAsync(string name);
        Task<bool> IsNameExistsAsyncForUpdate(string name, Guid id);
        Task<bool> IsCodeExistsAsync(string code);
        Task<bool> IsCodeExistsAsyncForUpdate(string name, Guid id);
    }
}
