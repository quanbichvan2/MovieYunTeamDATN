using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Commands
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, OneOf<bool, ResponseException>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly ILogger<UpdateMovieCommandHandler> _logger;
        private readonly IValidator<MovieForUpdateDto> _validator;
        private readonly IMapper _mapper;
        public UpdateMovieCommandHandler(IUnitOfWork unitOfWork,
            IMovieRepository movieRepository,
            IGenreRepository genreRepository,
            ICastMemberRepository castMemberRepository,
            IDirectorRepository directorRepository,
            ILogger<UpdateMovieCommandHandler> logger,
            IValidator<MovieForUpdateDto> validator,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _castMemberRepository = castMemberRepository;
            _directorRepository = directorRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.UpdateError, validationResult.Errors);
                }
                var movie = await _movieRepository.FindByIdAsync(request.Id);
                if (movie == null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.NotFound);
                }
                var director = await _directorRepository.FindByIdAsync(request.Model.DirectorId);
                if (director is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.Exception);
                }
                movie.DirectorName = director.Name;

                _mapper.Map(request.Model, movie);

                await UpdateGenres(movie, request.Model.Genres, cancellationToken);
                await UpdateCastMembers(movie, request.Model.CastMembers, cancellationToken);
                
                _unitOfWork.Entry(movie, EntityState.Modified);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.OperationFailed);
            }
        }
        private async Task UpdateGenres(Movie movie, IList<MovieGenreForUpdateDto> genreDtos, CancellationToken cancellationToken)
        {
            var existingGenres = movie.Genres.Select(g => g.GenreId).ToList();
            var newGenreIds = genreDtos.Select(g => g.Id!.Value).ToList();

            var genresToRemove = movie.Genres.Where(g => !newGenreIds.Contains(g.GenreId)).ToList();
            foreach (var genre in genresToRemove)
            {
                _unitOfWork.Entry(genre, EntityState.Deleted);
            }

            var genresToAdd = newGenreIds.Except(existingGenres).ToList();
            foreach (var genreId in genresToAdd)
            {
                var genre = await _genreRepository.FindByIdAsync(genreId);
                if (genre != null)
                {
                    var newMoviewGenre = new MovieGenre
                    {
                        GenreId = genre.Id,
                        GenreName = genre.Name,
                        MovieId = movie.Id
                    };
                    _unitOfWork.Entry(newMoviewGenre, EntityState.Added);
                }
            }
        }
        private async Task UpdateCastMembers(Movie movie, IList<MovieCastMemberForUpdateDto> castMemberDtos, CancellationToken cancellationToken)
        {
            var existingCastIds = movie.CastMembers.Select(c => c.CastMemberId).ToList();
            var newCastIds = castMemberDtos.Select(c => c.Id!.Value).ToList();

            var castToRemove = movie.CastMembers.Where(c => !newCastIds.Contains(c.CastMemberId)).ToList();
            foreach (var cast in castToRemove)
            {
                _unitOfWork.Entry(cast, EntityState.Deleted);
            }

            var castToAdd = newCastIds.Except(existingCastIds).ToList();
            foreach (var castId in castToAdd)
            {
                var castMember = await _castMemberRepository.FindByIdAsync(castId);
                if (castMember != null)
                {
                    var newMovieCastMember = new MovieCastMember
                    {
                        CastMemberId = castMember.Id,
                        CastMemberName = castMember.Name,
                        MovieId = movie.Id
                    };
                    _unitOfWork.Entry(newMovieCastMember, EntityState.Added);
                }
            }
        }
    }
}