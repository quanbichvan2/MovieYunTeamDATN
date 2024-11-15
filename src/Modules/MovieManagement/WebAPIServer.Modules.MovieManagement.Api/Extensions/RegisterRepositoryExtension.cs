using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebAPIServer.Modules.MovieManagement.Businesses;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Validations;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Validations;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Validations;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Validations;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Validations;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Validations;
using WebAPIServer.Modules.MovieManagement.DataAccesses.Repositories;

namespace WebAPIServer.Modules.MovieManagement.Api.Extensions
{
    public static class RegisterRepositoryExtension
    {
        public static IServiceCollection AddRegisterServicesMovieManagement(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MovieManagementServicesAssemblyMarker).GetTypeInfo().Assembly));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<IHallRepository, HallRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ISeatTypeRepository, SeatTypeRepository>();
            services.AddScoped<ICastMemberRepository, CastMemberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IValidator<DirectorForCreateDto>, DirectorForCreateDtoValidation>();
            services.AddScoped<IValidator<DirectorForUpdateDto>, DirectorForUpdateDtoValidation>();
            services.AddScoped<IValidator<GenreForCreateDto>, GenreForCreateDtoValidation>();
            services.AddScoped<IValidator<GenreForUpdateDto>, GenreForUpdateDtoValidation>();
            services.AddScoped<IValidator<HallForCreateDto>, HallForCreateDtoValidation>();
            services.AddScoped<IValidator<HallForUpdateDto>, HallForUpdateDtoValidation>();
            services.AddScoped<IValidator<MovieForCreateDto>, MovieForCreateDtoValidation>();
            services.AddScoped<IValidator<MovieForUpdateDto>, MovieForUpdateDtoValidation>();
            services.AddScoped<IValidator<ShowForCreateDto>, ShowForCreateDtoValidation>();
            services.AddScoped<IValidator<ShowForUpdateDto>, ShowForUpdateDtoValidation>();
            services.AddScoped<IValidator<CastMemberForCreateDto>, CastMemberForCreateDtoValidation>();
            services.AddScoped<IValidator<CastMemberForUpdateDto>, CastMemberForUpdateDtoValidation>();

            return services;
        }
    }
}