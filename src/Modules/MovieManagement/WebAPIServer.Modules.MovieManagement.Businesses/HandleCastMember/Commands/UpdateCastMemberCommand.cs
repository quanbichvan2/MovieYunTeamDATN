using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Commands
{
    public record UpdateCastMemberCommand(CastMemberForUpdateDto Model, Guid Id) : IRequest<OneOf<bool, ResponseException>>;
}