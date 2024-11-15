using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using WebAPIServer.Modules.Identity.Api.Extensions;
using WebAPIServer.Modules.Identity.Api.Middlewares;
using WebAPIServer.Modules.Identity.DataAccesses.Data;
using WebAPIServer.Modules.Identity.DataAccesses.Data.Seeders;
using WebAPIServer.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("WebAPIServer.Bootstrapper")]
namespace WebAPIServer.Modules.Identity.Api
{

	internal static class IdentityModule
	{
		internal static string JWT_KEY = "DuAnCuaNhom7AnhEmMovieNeuBanMuonThemThongTinChiTietThiToiChiu";
		public static IServiceCollection AddIdentityModule(this IServiceCollection services)
		{
			services.AddRegisterServicesIdentity();
			services.AddPostgres<IdentityDbContext>();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_KEY)),
					ValidateIssuer = false,
					ValidateAudience = false
				};
				options.SaveToken = true;
			});
			services.AddSession();
			return services;
		}
		public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var services = scope.ServiceProvider;
				SeedData.Initialize(services);
			}
			app.UseMiddleware<TokenCookieMiddleware>();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			return app;
		}
	}
}
