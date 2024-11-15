using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.DataAccesses.Data;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Users.DataAccesses.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<UsersDbContext>, IUnitOfWork
    {
        public UnitOfWork(UsersDbContext context) : base(context) { }
    }
}
