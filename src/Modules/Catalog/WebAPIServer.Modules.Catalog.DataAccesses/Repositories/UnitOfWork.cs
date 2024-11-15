using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.DataAccesses.Data;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<CatalogDbContext>, IUnitOfWork
	{
		public UnitOfWork(CatalogDbContext context) : base(context) { }
	}
}
