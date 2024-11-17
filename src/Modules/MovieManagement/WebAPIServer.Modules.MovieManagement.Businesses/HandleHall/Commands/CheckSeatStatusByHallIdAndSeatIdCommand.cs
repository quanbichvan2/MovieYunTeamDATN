using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Commands
{
    public record CheckSeatStatusByHallIdAndSeatIdCommand(CheckSeatStatusByHallIdAndSeatIdDto model) : IRequest<OneOf<Guid, ResponseException>>;
}
