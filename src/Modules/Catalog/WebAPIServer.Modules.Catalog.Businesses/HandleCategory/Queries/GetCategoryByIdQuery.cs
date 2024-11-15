using MediatR;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Queries
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<OneOf<CategoryForViewDto, ResponseException>>;

}
