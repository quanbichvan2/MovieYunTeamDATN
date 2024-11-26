using MediatR;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Queries
{
    public record GetSeatTypeByIdQuery(Guid id) : IRequest<SeatTypeForViewDto>;
}
