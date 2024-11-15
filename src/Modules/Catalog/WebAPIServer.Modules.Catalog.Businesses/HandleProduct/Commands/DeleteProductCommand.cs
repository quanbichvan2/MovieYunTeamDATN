using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Commands
{
    public record DeleteProductCommand(Guid Id): IRequest<OneOf<bool, ResponseException>>;
}
