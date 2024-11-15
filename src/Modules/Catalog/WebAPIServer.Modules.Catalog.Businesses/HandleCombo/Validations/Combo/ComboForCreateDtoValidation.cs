using FluentValidation;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Validations.ComboProduct;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Validations.Combo
{
    public class ComboForCreateDtoValidation: AbstractValidator<ComboForCreateDto>
    {
        public ComboForCreateDtoValidation(IComboRepository comboRepository)
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Không được để trống")
                .NotEmpty().WithMessage("Bắt buộc phải có")
                //check name đã tồn tại hay không :)))
                .MustAsync(async (name, cancellation) =>
                {
                    return !await comboRepository.IsNameExistsAsync(name!);
                }).WithMessage("Tên sản phẩm '{PropertyName}' đã tồn tại.");
            RuleFor(x => x.Code)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
                //check code đã tồn tại hay không
                .MustAsync(async (code, cancellation) =>
                {
                    return !await comboRepository.IsCodeExistsAsync(code!);
                }).WithMessage("Mã sản phẩm '{PropertyName}' đã tồn tại.");
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Hình Ảnh là bắt buộc");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage($"Phải lớn hơn 0")
                .GreaterThan(0).WithMessage(x => $"Không được bé hơn 0");
            RuleForEach(x => x.Products).SetValidator(new ComboProductForCreateDtoValidation());
        }
    }
}
