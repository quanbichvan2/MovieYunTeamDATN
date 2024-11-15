using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Services;
using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.Businesses.Services
{
	public class TokenService : ITokenService
	{
		internal string JWT_KEY = "DuAnCuaNhom7AnhEmMovieNeuBanMuonThemThongTinChiTietThiToiChiu";
		private readonly IAuthenticationRepository _authenticationRepository;
		public TokenService(IAuthenticationRepository authenticationRepository)
		{
			_authenticationRepository = authenticationRepository;
		}
		public async Task<string> GenerateAccessToken(UserIdentity user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(JWT_KEY);
			var roles = await _authenticationRepository.GetRolesAsync(user);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Email, user.Email)
			};
			if (roles.Any())
			{
				claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			}
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(15),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
	}
}
