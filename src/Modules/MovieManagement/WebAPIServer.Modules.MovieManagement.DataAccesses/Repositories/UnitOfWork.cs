using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<MovieManagementDbContext>, IUnitOfWork
    {
        public UnitOfWork(MovieManagementDbContext context) : base(context) { }
    }
}