using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPIServer.Modules.Tickets.Businesses;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Validations;
using WebAPIServer.Modules.Tickets.DataAccesses.Repositories;

namespace WebAPIServer.Modules.Tickets.Api.Extentions
{
	public static class RegisterServicesExtention
    {
        public static IServiceCollection AddRegisterServicesTickets(this IServiceCollection services)
        {
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));

            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IValidator<TicketTypeForCreateDto>, TicketForCreateDtoValidation>();
            services.AddScoped<IValidator<TicketTypeForUpdateDto>, TicketForUpdateDtoValidation>();
            return services;
        }
    }
}
