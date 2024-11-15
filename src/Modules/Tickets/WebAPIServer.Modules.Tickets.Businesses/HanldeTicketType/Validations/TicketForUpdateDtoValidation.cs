using FluentValidation;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Validations
{
	public class TicketForUpdateDtoValidation : AbstractValidator<TicketTypeForUpdateDto>
	{
		public TicketForUpdateDtoValidation(ITicketTypeRepository ticketRepository)
		{
			RuleFor(x => x.Name)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
			RuleFor(x => x.Code)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.")
				.Matches("^[a-zA-Z0-9]*$").WithMessage("Thuộc tính {PropertyName} chỉ cho phép chữ và số.");
		}
	}
}