using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Validations
{
    public class GenreForCreateDtoValidation : AbstractValidator<GenreForCreateDto>
    {
        public GenreForCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
        }
    }
}
