using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Repositories
{
    public class HallRepository : BaseRepository<Hall, MovieManagementDbContext>, IHallRepository
    {
        public HallRepository(MovieManagementDbContext context) : base(context) { }

        public override async Task<Hall?> FindByIdAsync(Guid? id)
        {
            var hall =  await _context.Halls
                .Include(x => x.Seats
                .OrderBy(seat => seat.SeatPosition))
                .FirstOrDefaultAsync(x => x.Id == id);
            return hall;
        }
    }
}