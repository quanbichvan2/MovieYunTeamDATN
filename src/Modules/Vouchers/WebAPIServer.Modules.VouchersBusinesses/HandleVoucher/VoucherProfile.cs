using AutoMapper;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Modules.Vouchers.Domain.Entities;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher
{
	public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            Init();
        }
        public void Init()
        {
            CreateMap<Voucher, VoucherForViewDto>();
            CreateMap<VoucherForCreateDto, Voucher>();
            CreateMap<VoucherForUpdateDto, Voucher>();
        }
    }
}
