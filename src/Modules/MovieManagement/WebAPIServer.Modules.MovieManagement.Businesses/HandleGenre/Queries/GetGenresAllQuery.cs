using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Queries
{
    public record GetGenresAllQuery(Filter Filter) : IRequest<PaginatedList<GenreForViewDto>>;
}
