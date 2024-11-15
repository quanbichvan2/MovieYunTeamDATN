using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Queries
{
    public record GetMovieByIdQuery(Guid Id): IRequest<OneOf<MovieForViewDetailDto, ResponseException>>;
}
