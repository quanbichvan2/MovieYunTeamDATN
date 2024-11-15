using WebAPIServer.Modules.Users.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories
{
	public interface IUserReponsitory : IRepository<User>
	{
		Task<bool> IsEmailExistsAsync(string email, Guid id);
	}
}
