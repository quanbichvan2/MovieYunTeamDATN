using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.DataAccesses.Data;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Vouchers.DataAccesses.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<VoucherDbContext>, IUnitOfWork
	{
		public UnitOfWork(VoucherDbContext context) : base(context) { }
	}
}
