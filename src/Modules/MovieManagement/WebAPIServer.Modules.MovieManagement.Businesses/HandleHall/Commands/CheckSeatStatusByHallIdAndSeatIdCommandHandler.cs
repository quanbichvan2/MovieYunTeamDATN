using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Commands
{
    public class CheckSeatStatusByHallIdAndSeatIdCommandHandler : IRequestHandler<CheckSeatStatusByHallIdAndSeatIdCommand, OneOf<Guid, ResponseException>>
    {
        public Task<OneOf<Guid, ResponseException>> Handle(CheckSeatStatusByHallIdAndSeatIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
