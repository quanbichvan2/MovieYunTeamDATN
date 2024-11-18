using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Queries
{
	public class GetShowsAllQueryHandler: IRequestHandler<GetShowsAllQuery, PaginatedList<ShowForViewDto>>
    {
        private readonly ILogger<GetShowsAllQueryHandler> _logger;
        private readonly IShowRepository _showRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public GetShowsAllQueryHandler(ILogger<GetShowsAllQueryHandler> logger,
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
        public async Task<PaginatedList<ShowForViewDto>> Handle(GetShowsAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _showRepository.GetAll();

                var allowedShowProperties = new List<string> { "MovieTitle" };
                if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
                {
                    string search = request.Filter.SearchTerm.ToLower().Trim();
                    //query = query.Where(x => EF.Functions.Unaccent(x.MovieTitle).ToLower().Contains(search));
                }
                query = query.SortBy(request.Filter?.SortColumn, allowedShowProperties, request.Filter.IsDescending);
                var paginatedShows = await PaginatedList<Show>.CreateAsync(
                    query,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    cancellationToken);

                var a = paginatedShows.Items.GroupBy(x => x.MovieId).ToList();
                List<ShowForViewDto> showForViewDtos = null ;
                foreach (var item in a)
                {
                    showForViewDtos = new List<ShowForViewDto>();
                    foreach (var itemDto in a)
                    {
                        var movie = await _movieRepository.GetByIdAsync(itemDto.First().MovieId);
                        ShowForViewDto showForViewDto = new ShowForViewDto();
                        showForViewDto.MovieId = movie.Id;
                        showForViewDto.MovieTitle = movie.Title;
                        showForViewDto.MoviePosterImage = movie.PosterImage;
                        showForViewDto.AgeRating = movie.AgeRating;
                        showForViewDto.MovieRuntimeMinutes = movie.RuntimeMinutes;
                        //var genres = await _genreRepository.GetByIdAsync(movie.Genres.First().Id);
                        foreach (var item1 in movie.Genres)
                        {
                            GenreMovieDto genre = new GenreMovieDto();
                            genre.Id = item1.Id;
                            genre.Name = item1.GenreName;
                            showForViewDto.Genres.Add(genre);
                        }

                        var show = _showRepository.GetAll().Where(x => x.MovieId == movie.Id).GroupBy(x => x.CinemaHallId).ToList();
                        foreach (var group in show)
                        {
                            var groupHall = group.GroupBy(x => x.StartTime.Date).ToList();
                            ListHall listHall = new ListHall();
                            foreach (var item1 in groupHall)
                            {
                                ListTime listTime = new ListTime();
                                foreach (var item2 in item1)
                                {
                                    var hall = await _hallRepository.GetByIdAsync(item2.CinemaHallId);
                                    listHall.HallId = hall.Id;
                                    listHall.HallName = hall.Name;


                                    listTime.StartTime = item2.StartTime.Day + "-" + item2.StartTime.Month + "-" + item2.StartTime.Year;
                                    var hour = item2.StartTime.Hour.ToString().Length == 2 ? item2.StartTime.Hour.ToString() : ("0" + item2.StartTime.Hour.ToString());
                                    var minute = item2.StartTime.Minute.ToString().Length == 2 ? item2.StartTime.Minute.ToString() : ("0" + item2.StartTime.Minute.ToString());

                                    ShowTime showTimeDto = new ShowTime();
                                    showTimeDto.Time = hour + ":" + minute;
                                    showTimeDto.ShowId = item2.Id;
                                    listTime.ShowTimes.Add(showTimeDto);


                                }
                                listHall.ListTime.Add(listTime);

                            }
                            showForViewDto.ListHall.Add(listHall);
                            
                        }
                        showForViewDtos.Add(showForViewDto);
                    }
                    
                }

                var paginatedShowViews = new PaginatedList<ShowForViewDto>(
                    showForViewDtos,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    paginatedShows.TotalCount);
                return paginatedShowViews;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
