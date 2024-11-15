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
        private readonly IMapper _mapper;
        public GetShowsAllQueryHandler(ILogger<GetShowsAllQueryHandler> logger,
            IShowRepository showRepository,
            IMapper mapper)
        {
            _logger = logger;
            _showRepository = showRepository;
            _mapper = mapper;
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
                var showForViewDtos = _mapper.Map<List<ShowForViewDto>>(paginatedShows.Items);

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
