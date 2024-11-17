using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models
{
    public class CheckSeatStatusByHallIdAndSeatIdDto
    {
        public Guid HallId { get; set; }
        public Guid SeatId { get; set; }
    }
}
