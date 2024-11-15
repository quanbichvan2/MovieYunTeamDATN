using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPIServer.Modules.Payment.Businesses;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Libraries;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Payment.DataAccesses.Repositories;

namespace WebAPIServer.Modules.Payment.Api.Extentions
{
	public static class RegisterServiceExtention
    {
        public static IServiceCollection AddRegisterServicesPayment(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));
            services.AddScoped<VnPayLibrary>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
