using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Commands;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Validations
{
    public class UpdateSeatTypeDtoValidation : AbstractValidator<UpdateSeatTypeDto>
    {
        public UpdateSeatTypeDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Thuộc tính {PropertyName} không hợp lệ. (> 0)");
        }
    }
}
