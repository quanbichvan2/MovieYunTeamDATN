using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Queries
{
    public class GetGenresAllQueryHandler : IRequestHandler<GetGenresAllQuery, PaginatedList<GenreForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<GetGenresAllQueryHandler> _logger;
        public GetGenresAllQueryHandler(IMapper mapper,
            IGenreRepository genreRepository,
            ILogger<GetGenresAllQueryHandler> logger)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
            _logger = logger;
        }

        public async Task<PaginatedList<GenreForViewDto>> Handle(GetGenresAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _genreRepository.GetAll();
                var allowedGenreProperties = new List<string> { "Name" };

				if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
				{
					string search = request.Filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Contains(search));
				}
				query = query.SortBy(request.Filter?.SortColumn, allowedGenreProperties, request.Filter.IsDescending);

                var paginatedGenres = await PaginatedList<Genre>.CreateAsync(
                    query,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    cancellationToken);

                var genreViewDtos = _mapper.Map<List<GenreForViewDto>>(paginatedGenres.Items);

                var paginatedGenreViews = new PaginatedList<GenreForViewDto>(
                    genreViewDtos,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    paginatedGenres.TotalCount);
                return paginatedGenreViews;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching genres: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
