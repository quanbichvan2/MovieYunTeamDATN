using WebAPIServer.Modules.Identity.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories
{
	public interface IRoleRepository: IRepository<RoleIdentity>
	{
		Task<bool> IsRoleAssignedToAnyUserAsync(Guid roleId);
	}
}