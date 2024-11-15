using Refit;
using WebAPIServer.Modules.Booking.Businesses.Dtos;

namespace WebAPIServer.Modules.Booking.Businesses.Contracts.Apis
{
	public interface ITicketsModuleApi
	{
		[Get("/TicketTypes/{id}")]
		Task<TicketTypeDto> GetTicketTypeById(Guid id);
	}
}