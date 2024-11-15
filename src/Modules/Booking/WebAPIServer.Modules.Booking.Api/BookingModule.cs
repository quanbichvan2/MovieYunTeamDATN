using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.Booking.Api.Extensions;
using WebAPIServer.Modules.Booking.Api.Extentions;
using WebAPIServer.Modules.Booking.DataAccesses.Data;
using WebAPIServer.Modules.Booking.DataAccesses.Data.Seeders;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Booking.Api
{
	internal static class BookingModule
	{
		public static IServiceCollection AddBookingModule(this IServiceCollection services)
		{
			services.AddRegisterRefitBooking();
			services.AddRegisterServicesBooking();
			services.AddPostgres<BookingDbContext>();
			return services;
		}
		public static IApplicationBuilder UseBookingModule(this IApplicationBuilder app)
		{
			// đăng ký các middleware của module
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var services = scope.ServiceProvider;
				SeedData.Initialize(services);
			}
			return app;
		}
	}
}
