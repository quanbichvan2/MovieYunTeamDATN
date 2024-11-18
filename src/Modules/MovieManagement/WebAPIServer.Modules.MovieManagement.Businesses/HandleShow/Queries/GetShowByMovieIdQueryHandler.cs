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
	public class GetShowByMovieIdQueryHandler : IRequestHandler<GetShowByMovieIdQuery, OneOf<ShowForViewDto, ResponseException>>
    {
        private readonly ILogger<GetShowByMovieIdQueryHandler> _logger;
        private readonly IShowRepository _showRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public GetShowByMovieIdQueryHandler(ILogger<GetShowByMovieIdQueryHandler> logger,
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
        public async Task<OneOf<ShowForViewDto, ResponseException>> Handle(GetShowByMovieIdQuery request, CancellationToken cancellationToken)
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

                //var show = _showRepository.GetAll().Where(x => x.MovieId == movie.Id).GroupBy(x => x.StartTime.Date).ToList();
                var show1 = _showRepository.GetAll().Where(x => x.MovieId == movie.Id).GroupBy(x => x.CinemaHallId).ToList();
                foreach (var group in show1)
                {
                    var groupHall = group.GroupBy(x => x.StartTime.Date).ToList();
                    ListHall listHall = new ListHall();
                    foreach (var item in groupHall)
                    {
                        ListTime listTime = new ListTime();
                        foreach (var item1 in item)
                        {
                            var hall = await _hallRepository.GetByIdAsync(item1.CinemaHallId);
                            listHall.HallId = hall.Id;
                            listHall.HallName = hall.Name;

                            
                            listTime.StartTime = item1.StartTime.Day + "-" + item1.StartTime.Month + "-" + item1.StartTime.Year;
                            var hour = item1.StartTime.Hour.ToString().Length == 2 ? item1.StartTime.Hour.ToString() : ("0" + item1.StartTime.Hour.ToString());
                            var minute = item1.StartTime.Minute.ToString().Length == 2 ? item1.StartTime.Minute.ToString() : ("0" + item1.StartTime.Minute.ToString());
                            ShowTime showTimeDto = new ShowTime();
                            showTimeDto.Time = hour + ":" + minute;
                            showTimeDto.ShowId = item1.Id;
                            listTime.ShowTimes.Add(showTimeDto);


                        }
                        listHall.ListTime.Add(listTime);
                        
                    }
                    showForViewDto.ListHall.Add(listHall);

                }

                //foreach (var group in show)
                //{
                //    ListTime listTime = new ListTime();
                //    foreach (var item in group)
                //    {
                //        listTime.StartTime = item.StartTime.Date;
                //        var hall = await _hallRepository.GetByIdAsync(item.CinemaHallId);
                //        //listTime.HallId = hall.Id;
                //        //listTime.HallName = hall.Name;
                //        var hour = item.StartTime.Hour.ToString().Length == 2 ? item.StartTime.Hour.ToString() : ("0" + item.StartTime.Hour.ToString());
                //        var minute = item.StartTime.Minute.ToString().Length == 2 ? item.StartTime.Minute.ToString() : ("0" + item.StartTime.Minute.ToString());
                //        listTime.ShowTimes.Add( hour + ":" + minute);
                //    }
                //    showForViewDto.ListTime.Add(listTime);
                //}

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