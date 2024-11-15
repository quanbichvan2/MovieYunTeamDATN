using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories
{
	public interface IAuthenticationRepository
	{
		Task<UserIdentity?> GetUserByEmail(string email);
		Task<bool> AddUserAsync(UserIdentity user);
		Task<List<string>> GetRolesAsync(UserIdentity user);
		Task<UserIdentity> GetUserByRefreshTokenAsync(string refreshToken);
		bool Update(UserIdentity user);
	}
}
