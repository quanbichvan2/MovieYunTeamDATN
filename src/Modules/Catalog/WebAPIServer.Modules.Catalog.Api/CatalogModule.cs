using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.Catalog.Api.Extentions;
using WebAPIServer.Modules.Catalog.DataAccesses.Data;
using WebAPIServer.Modules.Catalog.DataAccesses.Data.Seeders;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Catalog.Api
{
    internal static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services)
        {
            // đăng ký các dịch vụ của module Catalog;
            services.AddPostgres<CatalogDbContext>();
            services.AddRegisterServicesCatalog();
            return services;
        }
        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
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