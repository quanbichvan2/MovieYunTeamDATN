using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.DataAccesses.Data;
using WebAPIServer.Modules.Vouchers.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Vouchers.DataAccesses.Repositories
{
	public class VoucherRepository : BaseRepository<Voucher, VoucherDbContext>, IVoucherRepository
	{
		public VoucherRepository(VoucherDbContext context) : base(context)
		{

		}

		public async Task<bool> IsNameExistsAsync(string name, Guid Id)
		{
			return await _context.Vouchers.AnyAsync(p => p.Name == name && p.Id != Id);
		}
	}
}
