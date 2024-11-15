using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Commands
{
    public record UpdateDirectorCommand(Guid Id, DirectorForUpdateDto Model): IRequest<OneOf<bool, ResponseException>>;
}