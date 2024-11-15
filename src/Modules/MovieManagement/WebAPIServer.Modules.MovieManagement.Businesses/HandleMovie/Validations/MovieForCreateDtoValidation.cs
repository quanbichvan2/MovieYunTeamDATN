using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Validations
{
    public class MovieForCreateDtoValidation : AbstractValidator<MovieForCreateDto>
    {
        public MovieForCreateDtoValidation()
        {
            const byte MIN_SEAT = 30;

            RuleFor(x => x.Title)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.RuntimeMinutes)
                .GreaterThanOrEqualTo(MIN_SEAT).WithMessage("Thuộc tính {PropertyName} phải lớn hơn hoặc bằng " + $"{MIN_SEAT}.");
            RuleFor(x => x.TrailerLink)
                .Matches(@"(https?://)+(www.)+(youtube.com|youtu.be)\b").WithMessage("Thuộc tính {PropertyName} không hợp lệ.")
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.HeaderImage)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.PosterImage)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
            RuleFor(x => x.DirectorId)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
        }
    }
}
