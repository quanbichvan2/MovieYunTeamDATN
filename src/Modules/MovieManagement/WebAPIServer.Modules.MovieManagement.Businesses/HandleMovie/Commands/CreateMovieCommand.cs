using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Commands
{
    public record CreateMovieCommand(MovieForCreateDto model): IRequest<OneOf<Guid, ResponseException>>;
}
