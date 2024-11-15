using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Commands
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<OneOf<bool, ResponseException>>;
}
