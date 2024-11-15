using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Queries
{
    public class GetMoviesAllQueryHandler : IRequestHandler<GetMoviesAllQuery, PaginatedList<MovieForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<GetMoviesAllQueryHandler> _logger;
        public GetMoviesAllQueryHandler(IMapper mapper,
            IMovieRepository movieRepository,
            ILogger<GetMoviesAllQueryHandler> logger)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<PaginatedList<MovieForViewDto>> Handle(GetMoviesAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _movieRepository.GetAll();

                var allowedMovieProperties = new List<string> { "Title", "DirectorName" };
                if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
                {
                    string search = request.Filter.SearchTerm.ToLower().Trim();
                    query = query.Where(x => EF.Functions.Unaccent(x.Title).ToLower().Contains(search));
                }
                query = query.SortBy(request.Filter?.SortColumn, allowedMovieProperties, request.Filter.IsDescending);
                var paginatedMovies = await PaginatedList<Movie>.CreateAsync(
                    query,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    cancellationToken);
                var movieForViewDtos = _mapper.Map<List<MovieForViewDto>>(paginatedMovies.Items);

                var paginatedMovieViews = new PaginatedList<MovieForViewDto>(
                    movieForViewDtos,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    paginatedMovies.TotalCount);
                return paginatedMovieViews;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching Movies: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
