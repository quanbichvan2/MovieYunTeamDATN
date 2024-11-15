using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Commands
{
    public record DeleteGenreCommand(Guid id): IRequest<OneOf<bool, ResponseException>>;
}
