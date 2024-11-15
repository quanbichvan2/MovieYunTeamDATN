using MediatR;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Queries
{
	public record GetAllUsersQuery(Filter Filter) : IRequest<PaginatedList<UserForViewDto>>;
}
