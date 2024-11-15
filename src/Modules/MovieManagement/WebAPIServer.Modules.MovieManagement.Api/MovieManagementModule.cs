using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.MovieManagement.Api.Extensions;
using WebAPIServer.Modules.MovieManagement.Businesses;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.MovieManagement.Api
{
    internal static class MovieManagementModule
    {
        public static IServiceCollection AddMovieManagementModule(this IServiceCollection services)
        {
            services.AddPostgres<MovieManagementDbContext>();
            services.AddRegisterServicesMovieManagement();

            return services;
        }
        public static IApplicationBuilder UseMovieManagementModule(this IApplicationBuilder app)
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
