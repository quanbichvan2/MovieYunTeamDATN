using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Queries
{
    public record GetCastMembersAllQuery(Filter Filter) : IRequest<PaginatedList<CastMemberForViewDto>>;
}
