using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.Vouchers.Api.Extentions;
using WebAPIServer.Modules.Vouchers.DataAccesses.Data;
using WebAPIServer.Modules.Vouchers.DataAccesses.Data.Seeder;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Vouchers.Api
{
	internal static class VoucherModule
	{
		public static IServiceCollection AddVoucherModule(this IServiceCollection services)
		{
			services.AddPostgres<VoucherDbContext>();
			services.AddRegisterServicesVouchers();
			return services;
		}
		public static IApplicationBuilder UseVoucherModule(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var services = scope.ServiceProvider;
				SeedData.Initialize(services);
			}
			return app;
		}
	}
}
