using Microsoft.Extensions.DependencyInjection;
using Refit;
using WebAPIServer.Modules.Booking.Businesses.Contracts.Apis;

namespace WebAPIServer.Modules.Booking.Api.Extensions
{
	public static class RegisterRefitExtension
	{
		public static IServiceCollection AddRegisterRefitBooking(this IServiceCollection services)
		{
			services.AddRefitClient<ITicketsModuleApi>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7022/tickets-module"));
			services.AddRefitClient<ICatalogModuleApi>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7022/catalog-module"));
			services.AddRefitClient<IMovieManagementModuleApi>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7022/movie-management-module"));
            services.AddRefitClient<IVoucherModuleApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7022/voucher-module"));

            return services;
		}
	}
}
