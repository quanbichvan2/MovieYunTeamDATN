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
    public class GetShowByIdQueryHandler : IRequestHandler<GetShowByIdQuery, OneOf<ShowForViewApiDto, ResponseException>>
    {
        private readonly ILogger<GetShowByMovieIdQueryHandler> _logger;
        private readonly IShowRepository _showRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public GetShowByIdQueryHandler(ILogger<GetShowByMovieIdQueryHandler> logger,
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
        public async Task<OneOf<ShowForViewApiDto, ResponseException>> Handle(GetShowByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var show = await _showRepository.GetByIdAsync(request.Id);
                var movie = await _movieRepository.GetByIdAsync(show.MovieId);
                var hall = await _hallRepository.GetByIdAsync(show.CinemaHallId);
                ShowForViewApiDto showForView = new ShowForViewApiDto();
                showForView.Id = show.Id;
                showForView.CinemaHallId = hall.Id;
                showForView.HallName = hall.Name;
                showForView.MovieId = movie.Id;
                showForView.MovieTitle = movie.Title;
                showForView.MovieRuntimeMinutes = movie.RuntimeMinutes;
                showForView.StartTime = show.StartTime;
                showForView.EndTime = show.StartTime.AddMinutes(movie.RuntimeMinutes);

                return showForView;
            }
            catch (Exception ex)
            {
                    _logger.LogError(ex.Message);
                    return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.OperationFailed);
            }
        }
    }
}
