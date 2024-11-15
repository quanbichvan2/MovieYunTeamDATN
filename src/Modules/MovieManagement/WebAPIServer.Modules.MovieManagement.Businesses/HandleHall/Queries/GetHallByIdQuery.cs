using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Queries
{
    public record GetHallByIdQuery(Guid Id) : IRequest<OneOf<HallForViewDetailsDto, ResponseException>>;
}
