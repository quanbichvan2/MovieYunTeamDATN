using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Services;
using WebAPIServer.Modules.Identity.Businesses.Models;
using WebAPIServer.Modules.Identity.Businesses.Services;
using WebAPIServer.Modules.Identity.Businesses.Validations;
using WebAPIServer.Modules.Identity.DataAccesses.Repositories;

namespace WebAPIServer.Modules.Identity.Api.Extensions
{
	public static class RegisterServicesExtension
	{
        public static IServiceCollection AddRegisterServicesIdentity(this IServiceCollection services)
		{
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

			services.AddScoped<IValidator<LoginDto>, LoginDtoValidation>();
			services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidation>();


			return services;
		}
	}
}
