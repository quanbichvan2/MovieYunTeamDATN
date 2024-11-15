using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Validations
{
    public class CastMemberForCreateDtoValidation : AbstractValidator<CastMemberForCreateDto>
    {
        public CastMemberForCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
                .NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
        }
    }
}
