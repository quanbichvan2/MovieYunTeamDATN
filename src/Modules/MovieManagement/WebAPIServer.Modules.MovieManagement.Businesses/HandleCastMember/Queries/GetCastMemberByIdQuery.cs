using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Queries
{
    public record GetCastMemberByIdQuery(Guid Id): IRequest<OneOf<CastMemberForViewDto, ResponseException>>;
}
