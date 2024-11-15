using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPIServer.Modules.Catalog.Businesses;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Validations;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Validations.Combo;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Validations.ComboProduct;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Validations;
using WebAPIServer.Modules.Catalog.DataAccesses.Repositories;

namespace WebAPIServer.Modules.Catalog.Api.Extentions
{
	public static class RegisterServicesExtension
    {
        public static IServiceCollection AddRegisterServicesCatalog(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // đăng ký dịch vụ validate của product
            services.AddScoped<IValidator<ProductForCreateDto>, ProductForCreateDtoValidation>();
            services.AddScoped<IValidator<ProductForUpdateDto>, ProductForUpdateDtoValidation>();
            // đăng ký dịch vụ validate của Category
            services.AddScoped<IValidator<CategoryForCreateDto>, CategoryForCreateDtoValidation>();
            services.AddScoped<IValidator<CategoryForUpdateDto>, CategoryForUpdateDtoValidation>();
            // đăng kí dịch vụ validate của Combo
            services.AddScoped<IValidator<ComboForCreateDto>, ComboForCreateDtoValidation>();
            services.AddScoped<IValidator<ComboForUpdateDto>, ComboForUpdateDtoValidation>();
            services.AddScoped<IValidator<ComboProductForCreateDto>, ComboProductForCreateDtoValidation>();
            services.AddScoped<IValidator<ComboProductForUpdateDto>, ComboProductForUpdateDtoValidation>();

            return services;
        }

    }
}
