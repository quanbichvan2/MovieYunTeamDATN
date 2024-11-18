using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.Payment.Api.Extentions;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;
using WebAPIServer.Modules.Payment.DataAccesses.Data;
using WebAPIServer.Modules.Payment.DataAccesses.Data.Migrations.Seeders;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Payment.Api
{
    internal static class PaymentModule
    {
        public static IServiceCollection AddPaymentModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            services.AddPostgres<PaymentDbContext>();
            services.AddRegisterServicesPayment();
			services.AddHttpContextAccessor();
            return services;
        }
        public static IApplicationBuilder UsePaymentModule(this IApplicationBuilder app)
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
