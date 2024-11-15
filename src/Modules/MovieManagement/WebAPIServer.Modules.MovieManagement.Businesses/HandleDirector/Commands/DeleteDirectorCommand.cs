using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Commands
{
    public record DeleteDirectorCommand(Guid Id): IRequest<OneOf<bool, ResponseException>>;
}
