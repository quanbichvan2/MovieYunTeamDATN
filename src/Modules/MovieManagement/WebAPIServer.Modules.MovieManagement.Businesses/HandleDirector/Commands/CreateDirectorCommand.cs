using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Commands
{
    public record CreateDirectorCommand(DirectorForCreateDto Model): IRequest<OneOf<Guid, ResponseException>>;
}