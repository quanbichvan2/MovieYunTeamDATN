using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.Tickets.Api.Extentions;
using WebAPIServer.Modules.Tickets.DataAccesses.Data;
using WebAPIServer.Modules.Tickets.DataAccesses.Data.Seeders;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Tickets.Api
{
	internal static class TicketsModule
    {
        public static IServiceCollection AddTicketsModule(this IServiceCollection services)
        {
            services.AddPostgres<TicketsDbContext>();
            services.AddRegisterServicesTickets();
            return services;
        }
        public static IApplicationBuilder UseTicketsModule(this IApplicationBuilder app)
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