using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.DataAccesses.Data;
using WebAPIServer.Modules.Identity.Domain.Entities;

namespace WebAPIServer.Modules.Identity.DataAccesses.Repositories
{
	public class AuthenticationRepository : IAuthenticationRepository
	{
		private readonly IdentityDbContext _context;
		public AuthenticationRepository(IdentityDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// Get User By Id
		/// </summary>
		/// <param name="email"></param>
		/// <returns>UserIdentity</returns>
		public async Task<UserIdentity?> GetUserByEmail(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
		}
		/// <summary>
		/// Add new User
		/// </summary>
		/// <param name="user"></param>
		/// <returns>boolean</returns>
		public async Task<bool> AddUserAsync(UserIdentity user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return true;
		}
		/// <summary>
		/// Get User Roles
		/// </summary>
		/// <param name="user"></param>
		/// <returns>List string </returns>
		public async Task<List<string>> GetRolesAsync(UserIdentity user)
		{
			var userRole = await _context.UserRoles
				.Include(x => x.RoleIdentity)
				.Where(x => x.UserId == user.Id).ToListAsync();
			return userRole.Select(x => x.RoleIdentity.NormalizeName).ToList();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public async Task<UserIdentity?> GetUserByRefreshTokenAsync(string? refreshToken)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
		}
		public bool Update(UserIdentity user)
		{
			_context.Users.Update(user);
			_context.SaveChanges();
			return true;
		}
	}
}
