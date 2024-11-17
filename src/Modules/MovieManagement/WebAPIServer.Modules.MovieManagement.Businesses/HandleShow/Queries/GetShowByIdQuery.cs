using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Queries
{
    public record GetShowByIdQuery(Guid Id) : IRequest<OneOf<ShowForViewApiDto, ResponseException>>;
}
