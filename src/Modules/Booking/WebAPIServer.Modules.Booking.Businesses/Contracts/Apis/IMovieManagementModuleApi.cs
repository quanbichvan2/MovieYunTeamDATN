using Refit;
using WebAPIServer.Modules.Booking.Businesses.Dtos;

namespace WebAPIServer.Modules.Booking.Businesses.Contracts.Apis
{
	public interface IMovieManagementModuleApi
	{
		[Get("/shows/{id}")]
		Task<ShowDto> GetShowByIdAsync(Guid id);

		[Get("/SeatTypes/{id}")]
		Task<SeatDto> GetSeatByIdAsync(Guid id);

		[Get("/halls/{id}")]
		Task<HallDto> GetHallByIdAsync(Guid id);
	}
}