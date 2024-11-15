using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Commands
{
    public record UpdateMovieCommand(MovieForUpdateDto Model, Guid? Id): IRequest<OneOf<bool, ResponseException>>;
}
