using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Shared.Infrastructure.Api;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Shared.Infrastructure
{
    internal static class Extensions
    {
        internal static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllers().ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            services.AddPostgres();

			// Add CORS
			services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin =>
					true).AllowCredentials();
				});
			});

			return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCors(MyAllowSpecificOrigins);
            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection(sectionName);
            var options = new T();
            section.Bind(options);

            return options;
        }
    }
}