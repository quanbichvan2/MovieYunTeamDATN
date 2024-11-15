using FluentValidation;
using FluentValidation.Validators;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Validations
{
    public class CategoryForUpdateDtoValidation : AbstractValidator<CategoryForUpdateDto>
    {
        public CategoryForUpdateDtoValidation(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
            //check name đã tồn tại hay không :)))
                .MustAsync(async (name, cancellation) =>
                {
                    return !await categoryRepository.IsNameExistsAsync(name!);
                }).WithMessage("Tên sản phẩm '{PropertyName}' đã tồn tại.");
            RuleFor(x => x.Code)
                .NotNull().WithMessage("Thuộc tính {PropertyCode} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyCode} không được phép trống.")
            //check code đã tồn tại hay không
                .MustAsync(async (code, cancellation) =>
                {
                    return !await categoryRepository.IsCodeExistsAsync(code!);
                }).WithMessage("Mã sản phẩm '{Propertycode}' đã tồn tại.");
        }
    }
}
