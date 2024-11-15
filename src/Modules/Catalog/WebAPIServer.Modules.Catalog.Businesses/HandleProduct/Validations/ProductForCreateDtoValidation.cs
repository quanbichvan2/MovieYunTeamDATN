using FluentValidation;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Validations
{
    public class ProductForCreateDtoValidation: AbstractValidator<ProductForCreateDto>
    {
        private readonly IProductRepository _productRepository;
        public ProductForCreateDtoValidation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
                .MustAsync(async (name, cancellation) =>
                        !await _productRepository.IsNameExistsAsync(name!))
                        .WithMessage("Tên sản phẩm đã tồn tại.");
            //.Length(3, 100).WithMessage("Độ dài {PropertyName} phải từ 3 đến dưới 100 kí tự");

            RuleFor(x => x.Price)   
                .GreaterThanOrEqualTo(1).WithErrorCode("Thuộc tính {PropertyName} phải lớn hơn 1 ");
            RuleFor(x => x.Code)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Thuộc tính {PropertyCode} chỉ cho phép chữ và số.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
                .MustAsync(async (code, cancellation) =>
                        !await _productRepository.IsCodeExistsAsync(code))
                        .WithMessage("Mã sản phẩm đã tồn tại.");
            //.Length(3, 50).WithMessage("Độ dài {PropertyName} phải từ 3 đến dưới 100 kí tự");
        }
    }
}