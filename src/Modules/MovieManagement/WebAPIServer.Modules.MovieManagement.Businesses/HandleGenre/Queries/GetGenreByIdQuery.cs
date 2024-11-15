using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Queries
{
    public record GetGenreByIdQuery(Guid Id): IRequest<OneOf<GenreForViewDto, ResponseException>>;
}
