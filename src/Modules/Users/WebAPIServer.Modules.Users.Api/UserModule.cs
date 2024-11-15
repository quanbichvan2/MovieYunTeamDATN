using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using WebAPIServer.Modules.Users.Api.Extentions;
using WebAPIServer.Modules.Users.DataAccesses.Data;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Users.Api
{
	internal static class UserModule
	{
		public static IServiceCollection AddUsersModule(this IServiceCollection services)
		{
			services.AddPostgres<UsersDbContext>();
			services.AddRegisterServicesUsers();
			return services;
		}
		public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
		{
			return app;
		}
	}
}
