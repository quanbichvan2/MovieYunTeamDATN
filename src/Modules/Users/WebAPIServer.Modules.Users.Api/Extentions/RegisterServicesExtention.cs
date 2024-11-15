using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPIServer.Modules.Users.Businesses;
using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Validations;
using WebAPIServer.Modules.Users.DataAccesses.Repositories;

namespace WebAPIServer.Modules.Users.Api.Extentions
{
	public static class RegisterServicesExtention
    {
        public static IServiceCollection AddRegisterServicesUsers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));

            services.AddScoped<IUserReponsitory, UserReponsitory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IValidator<UserForCreateDto>, UserForCreateDtoValidation>();
            services.AddScoped<IValidator<UserForUpdateDto>, UserForUpdateDtoValidation>();
            return services;
        }
    }
}
