using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.Booking.Businesses.Dtos;

namespace WebAPIServer.Modules.Booking.Businesses.Contracts.Apis
{
    public interface IVoucherModuleApi
    {
        [Get("/voucher/{id}")]
        Task<VoucherDto> GetVoucherByIdAsync(Guid id);
    }
}
