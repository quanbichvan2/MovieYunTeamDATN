using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Reflection;
using WebAPIServer.Modules.Booking.Businesses;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
//using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Backgounds;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Commands;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Validations;
using WebAPIServer.Modules.Booking.DataAccesses.Repositories;
using WebAPIServer.Modules.Booking.Domain.Entities;

namespace WebAPIServer.Modules.Booking.Api.Extentions
{
	public static class RegisterServicesExtension
    {
        public static IServiceCollection AddRegisterServicesBooking(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IValidator<OrderForCreateDto>, OrderForCreateDtoValidation>();
            services.AddScoped<IValidator<OrderForUpdateDto>, OrderForUpdateDtoValidation>();
			//services.AddHostedService<OrderStatusBackgroundService>();
			return services;
        }

    }
}
