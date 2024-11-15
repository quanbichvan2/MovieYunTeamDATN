using FluentValidation;
using System.Xml.Linq;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Validations
{
    public class ProductForUpdateDtoValidation : AbstractValidator<ProductForUpdateDto>
    {
        private readonly IProductRepository _productRepository;
        public ProductForUpdateDtoValidation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithErrorCode("Thuộc tính {PropertyName} phải lớn hơn 1 ");
            RuleFor(x => x.Code)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Thuộc tính {PropertyName} chỉ cho phép chữ và số.");
        }
    }
}
