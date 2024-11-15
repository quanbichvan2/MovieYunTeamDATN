using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Commands
{
    public record CreateGenreCommand(GenreForCreateDto model): IRequest<OneOf<Guid, ResponseException>>;
}