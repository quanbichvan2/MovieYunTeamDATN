using OneOf;
using WebAPIServer.Modules.Identity.Businesses.Models;
using WebAPIServer.Modules.Identity.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Identity.Businesses.Contracts.Services
{
	public interface IAuthenticationService
	{
		Task<OneOf<bool, ResponseException>> Register(RegisterDto modelDto, CancellationToken cancellationToken);
		Task<OneOf<bool, ResponseException>> ResetPassword(string emailClaim);
        Task<OneOf<ResponseJwtHelper, ResponseException>> Login(LoginDto modelDto);
		Task<UserIdentity> ValidateRefreshToken(string refreshToken);
	}
}
