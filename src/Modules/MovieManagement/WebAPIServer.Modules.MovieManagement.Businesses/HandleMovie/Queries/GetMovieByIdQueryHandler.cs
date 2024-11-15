using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Queries
{
	public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, OneOf<MovieForViewDetailDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<GetMovieByIdQueryHandler> _logger;
        public GetMovieByIdQueryHandler(IMapper mapper, IMovieRepository movieRepository, ILogger<GetMovieByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<OneOf<MovieForViewDetailDto, ResponseException>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _movieRepository.FindByIdAsync(request.Id);
                if (movie is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.NotFound);
                }
                var movieForView = _mapper.Map<MovieForViewDetailDto>(movie);
                return movieForView;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.OperationFailed);
            }
        }
    }
}
