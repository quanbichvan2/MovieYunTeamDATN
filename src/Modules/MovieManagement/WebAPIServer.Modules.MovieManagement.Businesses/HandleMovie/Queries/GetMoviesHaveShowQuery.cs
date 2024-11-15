using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Queries
{
	public record GetMoviesHaveShowQuery(Filter Filter, DateTime SelectedDate) : IRequest<PaginatedList<MovieForViewDto>>;
}
