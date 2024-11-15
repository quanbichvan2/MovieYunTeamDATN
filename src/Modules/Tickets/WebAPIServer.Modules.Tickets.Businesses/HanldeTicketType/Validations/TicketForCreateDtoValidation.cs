using FluentValidation;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Validations
{
	public class TicketForCreateDtoValidation : AbstractValidator<TicketTypeForCreateDto>
	{
		public TicketForCreateDtoValidation(ITicketTypeRepository ticketRepository)
		{
			RuleFor(x => x.Name)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
			RuleFor(x => x.Code)
				.NotNull().WithMessage("Thuộc tính {PropertyName} không được phép null.")
				.NotEmpty().WithMessage("Thuộc tính {PropertyName} không được phép trống.");
		}
	}
}
