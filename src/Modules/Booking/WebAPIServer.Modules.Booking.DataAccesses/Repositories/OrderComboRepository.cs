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
    public class OrderComboRepository : BaseRepository<OrderCombo, BookingDbContext>, IOrderComboRepository
    {
        public OrderComboRepository(BookingDbContext context) : base(context) { }

        public override Task<OrderCombo?> GetByIdAsync(Guid? id)
        {
            return _context.OrderCombos
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
