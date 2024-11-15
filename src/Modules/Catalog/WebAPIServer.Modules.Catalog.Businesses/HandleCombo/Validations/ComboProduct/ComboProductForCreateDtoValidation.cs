using FluentValidation;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Validations.ComboProduct
{
    public class ComboProductForCreateDtoValidation: AbstractValidator<ComboProductForCreateDto>
    {
        public ComboProductForCreateDtoValidation()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Bắt buộc phải có");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage($"Không được để trống")
                .GreaterThanOrEqualTo(1).WithMessage($"Phải lớn hơn hoặc bằng 1");
        }
    }
}
