using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Queries
{
    public record GetDirectorByIdQuery(Guid Id): IRequest<OneOf<DirectorForViewDto, ResponseException>>;
}
