using MediatR;
using OneOf;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Queries
{
	public record GetUserByIdQuery(Guid Id) : IRequest<OneOf<UserForViewDto, ResponseException>>;
}
