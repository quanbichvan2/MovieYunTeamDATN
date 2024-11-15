using MediatR;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Commands
{
    public record UpdateProductCommand(Guid Id, ProductForUpdateDto Model) : IRequest<OneOf<bool, ResponseException>>;
}
