using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.DataAccesses.Data;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Booking.DataAccesses.Repositories
{
    public class OrderMovieRepository : BaseRepository<OrderMovie, BookingDbContext>, IOrderMovieRepository
    {
        public OrderMovieRepository(BookingDbContext context) : base(context) { }

        public override Task<OrderMovie?> GetByIdAsync(Guid? id)
        {
            return _context.OrderMovies
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
