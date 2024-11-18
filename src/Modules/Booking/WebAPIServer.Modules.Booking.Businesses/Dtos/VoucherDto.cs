using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIServer.Modules.Booking.Businesses.Dtos
{
    public class VoucherDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public double DiscountValue { get; set; }
    }
}
