using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Queries
{
    public record GetSeatTypeByIdQuery(Guid id) : IRequest<SeatTypeForViewDto>;
}
