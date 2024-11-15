using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Queries
{
	public class GetShowByIdQueryHandler : IRequestHandler<GetShowByIdQuery, OneOf<ShowForViewDto, ResponseException>>
    {
        private readonly ILogger<GetShowByIdQueryHandler> _logger;
        private readonly IShowRepository _showRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public GetShowByIdQueryHandler(ILogger<GetShowByIdQueryHandler> logger,
            IShowRepository showRepository,
            IMapper mapper,
            IHallRepository hallRepository,
            IMovieRepository movieRepository)
        {
            _logger = logger;
            _showRepository = showRepository;
            _mapper = mapper;
            _hallRepository = hallRepository;
            _movieRepository = movieRepository;
        }
        public async Task<OneOf<ShowForViewDto, ResponseException>> Handle(GetShowByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var movie = await _movieRepository.FindByIdAsync(request.Id);
                if (movie is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.NotFound);
                }
                ShowForViewDto showForViewDto = new ShowForViewDto();
                showForViewDto.MovieId = movie.Id;
                showForViewDto.MovieTitle = movie.Title;
                showForViewDto.MoviePosterImage = movie.PosterImage;
                showForViewDto.AgeRating = movie.AgeRating;
                showForViewDto.MovieRuntimeMinutes = movie.RuntimeMinutes;
                foreach (var item in movie.Genres)
                {
                    GenreMovieDto genre = new GenreMovieDto();
                    genre.Id = item.Id;
                    genre.Name = item.GenreName;
                    showForViewDto.Genres.Add(genre);
                }

                var show = _showRepository.GetAll().Where(x => x.MovieId == movie.Id).ToList();

                foreach (var item in show)
                {
                    ShowTimeDto showTimeDto = new ShowTimeDto();
                    showTimeDto.StartTime = item.StartTime;
                    showTimeDto.Time = item.StartTime.Hour + ":" + item.StartTime.Minute;
                    showForViewDto.ShowTimes.Add(showTimeDto);
                }

                return showForViewDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.OperationFailed);
            }
        }
    }
}