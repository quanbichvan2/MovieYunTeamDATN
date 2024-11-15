using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPIServer.Modules.Vouchers.Businesses;
using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Validations;
using WebAPIServer.Modules.Vouchers.DataAccesses.Repositories;

namespace WebAPIServer.Modules.Vouchers.Api.Extentions
{
	public static class RegisterServicesExtention
	{
		public static IServiceCollection AddRegisterServicesVouchers(this IServiceCollection services)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));
			services.AddScoped<IVoucherRepository, VoucherRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			var config = new MapperConfiguration(c =>
			{
				c.AddProfile<VoucherProfile>();
			});
			services.AddSingleton<IMapper>(s => config.CreateMapper());

			services.AddScoped<IValidator<VoucherForCreateDto>, VoucherForCreateDtoValidation>();
			services.AddScoped<IValidator<VoucherForUpdateDto>, VoucherForUpdateDtoValidation>();
			return services;
		}
	}
}
