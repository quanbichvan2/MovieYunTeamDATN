using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.DataAccesses.Data;
using WebAPIServer.Modules.Users.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Users.DataAccesses.Repositories
{
    public class UserReponsitory : BaseRepository<User, UsersDbContext>, IUserReponsitory
    {
        public UserReponsitory(UsersDbContext context) : base(context)
        {

        }

        public async Task<bool> IsEmailExistsAsync(string email, Guid id)
        {
            return await _context.Users.AnyAsync(p => p.Email == email && p.Id != id);
        }
    }
}
