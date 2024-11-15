using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Queries
{
	public class GetCastMembersAllQueryHandler : IRequestHandler<GetCastMembersAllQuery, PaginatedList<CastMemberForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly ILogger<GetCastMembersAllQueryHandler> _logger;
        public GetCastMembersAllQueryHandler(IMapper mapper,
            ICastMemberRepository castMemberRepository,
            ILogger<GetCastMembersAllQueryHandler> logger)
        {
            _mapper = mapper;
            _castMemberRepository = castMemberRepository;
            _logger = logger;
        }

        public async Task<PaginatedList<CastMemberForViewDto>> Handle(GetCastMembersAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _castMemberRepository.GetAll();
                var allowedCastMemberProperties = new List<string> { "Name" };
				if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
				{
					string search = request.Filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Contains(search));
				}
				query = query.SortBy(request.Filter?.SortColumn, allowedCastMemberProperties, request.Filter.IsDescending);
                var paginatedCastMembers = await PaginatedList<CastMember>.CreateAsync(
                    query,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    cancellationToken);

                var castMemberViewDtos = _mapper.Map<List<CastMemberForViewDto>>(paginatedCastMembers.Items);

                var paginatedCastMemberViews = new PaginatedList<CastMemberForViewDto>(
                    castMemberViewDtos,
                    request.Filter.PageIndex,
                    request.Filter.PageSize,
                    paginatedCastMembers.TotalCount);
                return paginatedCastMemberViews;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching CastMembers: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
