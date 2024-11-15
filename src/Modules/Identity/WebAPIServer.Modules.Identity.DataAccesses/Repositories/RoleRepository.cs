using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.DataAccesses.Data;
using WebAPIServer.Modules.Identity.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Identity.DataAccesses.Repositories
{
	public class RoleRepository: BaseRepository<RoleIdentity, IdentityDbContext>, IRoleRepository
	{
		public RoleRepository(IdentityDbContext context): base(context) { }
		public async Task<bool> IsRoleAssignedToAnyUserAsync(Guid roleId)
		{
			// Assuming UserRoles table exists and tracks user-role relationships
			return await _context.UserRoles
				.AnyAsync(ur => ur.RoleId == roleId);  // Check if any users are assigned this role
		}
	}
}
