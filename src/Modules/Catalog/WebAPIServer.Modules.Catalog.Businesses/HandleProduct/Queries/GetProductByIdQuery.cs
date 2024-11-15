using MediatR;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries
{
    public record GetProductByIdQuery(Guid id) : IRequest<OneOf<ProductForViewDto, ResponseException>>;
}
