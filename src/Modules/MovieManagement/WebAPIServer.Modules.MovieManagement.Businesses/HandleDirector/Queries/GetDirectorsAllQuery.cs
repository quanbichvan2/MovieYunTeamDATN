using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Queries
{
	public record GetDirectorsAllQuery(Filter Filter) : IRequest<PaginatedList<DirectorForViewDto>>;
}
