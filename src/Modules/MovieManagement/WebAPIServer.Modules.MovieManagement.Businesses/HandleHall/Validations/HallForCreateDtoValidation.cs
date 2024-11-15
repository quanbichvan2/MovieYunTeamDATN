using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Validations
{
    public class HallForCreateDtoValidation : AbstractValidator<HallForCreateDto>
    {
        public HallForCreateDtoValidation()
        {
            const byte MAX_SEAT = 15;
            const byte MIN_SEAT = 1;

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");

            // Validate cho SeatColumn
            RuleFor(x => x.SeatColumn)
                .GreaterThanOrEqualTo(MIN_SEAT).WithMessage("Thuộc tính {PropertyName} phải lớn hơn hoặc bằng " + $"{MIN_SEAT}.")
                .LessThanOrEqualTo(MAX_SEAT).WithMessage("Thuộc tính {PropertyName} phải nhỏ hơn hoặc bằng " + $"{MAX_SEAT}.");

            // Validate cho SeatRow
            RuleFor(x => x.SeatRow)
                .GreaterThanOrEqualTo(MIN_SEAT).WithMessage("Thuộc tính {PropertyName} phải lớn hơn hoặc bằng " + $"{MIN_SEAT}.")
                .LessThanOrEqualTo(MAX_SEAT).WithMessage("Thuộc tính {PropertyName} phải nhỏ hơn hoặc bằng " + $"{MAX_SEAT}.");

        }
    }
}
