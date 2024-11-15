using FluentValidation;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Validations.ComboProduct
{
    public class ComboProductForUpdateDtoValidation : AbstractValidator<ComboProductForUpdateDto>
    {
        public ComboProductForUpdateDtoValidation()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Không được để trống")
               .NotEmpty().WithMessage("Bắt buộc phải có");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1).WithMessage("Số lượng sản phẩm phải lớn hơn hoặc bằng 1");
        }
    }
}
