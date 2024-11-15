using FluentValidation;
using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Validations
{
	public class VoucherForUpdateDtoValidation : AbstractValidator<VoucherForUpdateDto>
    {
        public VoucherForUpdateDtoValidation(IVoucherRepository voucherRepository)
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.Code)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Thuộc tính {PropertyName} chỉ cho phép chữ và số.");
            RuleFor(x => x.DiscountValue)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
                .GreaterThan(1).WithMessage("Thuộc tính {PropertyName} không được bé hơn 1");
        }
    }
}
