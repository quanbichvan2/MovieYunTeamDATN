using Refit;
using WebAPIServer.Modules.Booking.Businesses.Dtos;

namespace WebAPIServer.Modules.Booking.Businesses.Contracts.Apis
{
	public interface ICatalogModuleApi
	{
		[Get("/products/{id}")]
		Task<ProductDto> GetProductByIdAsync(Guid id);
		[Get("/combos/{id}")]
		Task<ComboDto> GetComboByIdAsync(Guid id);
	}
}