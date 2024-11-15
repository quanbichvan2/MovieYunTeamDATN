using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
        Task<bool> IsUniqueComboName(string name);
        Task<bool> IsProductComboExist(Guid comboId, Guid? productId);
        Task<bool> IsNameExistsAsync(string name);
        Task<bool> IsCodeExistsAsync(string code);
    }
}
