using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Queries
{
	public class GetCastMembersAllQueryHandler : IRequestHandler<GetDirectorsAllQuery, PaginatedList<DirectorForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDirectorRepository _directorRepository;
        private readonly ILogger<GetCastMembersAllQueryHandler> _logger;
        public GetCastMembersAllQueryHandler(IMapper mapper,
            IDirectorRepository directorRepository,
            ILogger<GetCastMembersAllQueryHandler> logger)
        {
            _mapper = mapper;
            _directorRepository = directorRepository;
            _logger = logger;
        }

        public async Task<PaginatedList<DirectorForViewDto>> Handle(GetDirectorsAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _directorRepository.GetAll();
                var allowedDirectorProperties = new List<string> { "Name" };
				if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
				{
					string search = request.Filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Contains(search));
				}
				query = query.SortBy(request.Filter?.SortColumn, allowedDirectorProperties, request.Filter.IsDescending);
                var paginatedDirectors = await PaginatedList<Director>.CreateAsync(
                    query,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    cancellationToken);

                var directorViewDtos = _mapper.Map<List<DirectorForViewDto>>(paginatedDirectors.Items);

                var paginatedDirectorViews = new PaginatedList<DirectorForViewDto>(
                    directorViewDtos,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    paginatedDirectors.TotalCount);
                return paginatedDirectorViews;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching Directors: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
