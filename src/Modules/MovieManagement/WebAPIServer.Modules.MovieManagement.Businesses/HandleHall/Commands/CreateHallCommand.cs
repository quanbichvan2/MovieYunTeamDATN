﻿using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Commands
{
    public record CreateHallCommand(HallForCreateDto model) : IRequest<OneOf<Guid, ResponseException>>;
}
