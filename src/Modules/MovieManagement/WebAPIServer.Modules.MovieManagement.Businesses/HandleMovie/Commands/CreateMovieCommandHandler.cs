using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Commands
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly ILogger<CreateMovieCommandHandler> _logger;
        private readonly IValidator<MovieForCreateDto> _validator;
        private readonly IMapper _mapper;
        public CreateMovieCommandHandler(IMovieRepository movieRepository,
            IGenreRepository genreRepository,
            ICastMemberRepository castMemberRepository,
            ILogger<CreateMovieCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IValidator<MovieForCreateDto> validator,
            IMapper mapper,
            IDirectorRepository directorRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _castMemberRepository = castMemberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _directorRepository = directorRepository;
        }
        public async Task<OneOf<Guid, ResponseException>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.UpdateError, validationResult.Errors);
                }
                var movieDto = request.model;
                var movie = _mapper.Map<Movie>(movieDto);

                var director = await _directorRepository.FindByIdAsync(request.model.DirectorId);
                if (director is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.Exception, "Cần phải có ít nhất một thể loại.");
                }
                movie.DirectorName = director.Name;

                foreach (var castMember in movie.CastMembers)
                {
                    CastMember? castMemberEntity = null;
                    if (castMember?.CastMemberId != null)
                    {
                        castMemberEntity = await _castMemberRepository.FindByIdAsync(castMember.Id);
                    }
                    if (castMemberEntity == null)
                    {
                        castMemberEntity = new CastMember
                        {
                            Name = castMember.CastMemberName!
                        };
                        await _castMemberRepository.CreateAsync(castMemberEntity);
                    }
                    castMember.CastMemberName = castMemberEntity.Name;
                    castMember.CastMemberId = castMemberEntity.Id;
                }

                foreach (var genre in movie.Genres)
                {
                    var genreEntity = await _genreRepository.FindByIdAsync(genre.GenreId);
                    if (genreEntity == null)
                    {
                        return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.NotFound);
                    }
                    genre.GenreName = genreEntity.Name;
                }
                await _movieRepository.CreateAsync(movie);
                await _unitOfWork.SaveChangesAsync();

                return movie.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.OperationFailed);
            }
        }
    }
}
