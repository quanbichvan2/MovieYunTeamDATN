using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Queries
{
    public record GetShowByMovieIdQuery(Guid Id): IRequest<OneOf<ShowForViewDto, ResponseException>>;
}
