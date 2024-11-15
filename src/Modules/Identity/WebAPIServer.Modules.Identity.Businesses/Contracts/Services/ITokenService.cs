using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.Businesses.Contracts.Services
{
	public interface ITokenService
	{
		Task<string> GenerateAccessToken(UserIdentity user);
		string GenerateRefreshToken();
	}
}
