using MediatR;
using OneOf;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Commands
{
	public record CreateUserCommand(UserForCreateDto Model) : IRequest<OneOf<Guid, ResponseException>>;
}
