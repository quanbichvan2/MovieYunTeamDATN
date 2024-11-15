using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Commands
{
	public record DeleteUserCommand(Guid Id): IRequest<OneOf<bool,ResponseException>>;
}
