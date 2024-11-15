using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Commands
{
    public record CreateCastMemberCommand(CastMemberForCreateDto Model) : IRequest<OneOf<Guid, ResponseException>>;
}