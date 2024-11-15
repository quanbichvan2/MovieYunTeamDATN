using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Validations
{
    public class GenreForUpdateDtoValidation : AbstractValidator<GenreForUpdateDto>
    {
        public GenreForUpdateDtoValidation()
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