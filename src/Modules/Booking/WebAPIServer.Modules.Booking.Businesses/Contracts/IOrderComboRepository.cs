using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.Booking.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Booking.Businesses.Contracts
{
    public interface IOrderComboRepository : IRepository<OrderCombo>
    {
    }
}
