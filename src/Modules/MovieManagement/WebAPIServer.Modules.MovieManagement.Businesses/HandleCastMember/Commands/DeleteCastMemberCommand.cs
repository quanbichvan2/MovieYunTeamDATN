using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Commands
{
    public record DeleteCastMemberCommand(Guid Id) : IRequest<OneOf<bool, ResponseException>>;
}
