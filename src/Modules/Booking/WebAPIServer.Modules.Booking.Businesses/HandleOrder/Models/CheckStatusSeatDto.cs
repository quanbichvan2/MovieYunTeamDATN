using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models
{
    public class CheckStatusSeatDto
    {
        public Guid HallId { get; set; }
        public string SeatName { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
