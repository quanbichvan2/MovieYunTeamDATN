using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Queries
{
    public record GetShowsAllQuery(Filter Filter) : IRequest<PaginatedList<ShowForViewDto>>;
}
