using WebAPIServer.Modules.Vouchers.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories
{
	public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<bool> IsNameExistsAsync(string name, Guid Id);
    }
}
