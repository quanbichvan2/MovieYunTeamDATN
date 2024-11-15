using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Queries
{
    public class GetHallsAllQuery(): IRequest<IEnumerable<HallForViewDto>>;
}
