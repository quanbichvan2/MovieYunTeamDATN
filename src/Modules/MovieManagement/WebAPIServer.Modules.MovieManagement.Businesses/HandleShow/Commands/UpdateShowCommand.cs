﻿using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Commands
{
    public record UpdateShowCommand(ShowForUpdateDto Model, Guid Id): IRequest<OneOf<bool, ResponseException>>;
}
