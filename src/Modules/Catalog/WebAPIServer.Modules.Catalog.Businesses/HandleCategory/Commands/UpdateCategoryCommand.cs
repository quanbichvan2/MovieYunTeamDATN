using MediatR;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Commands
{
    public record UpdateCategoryCommand(Guid Id, CategoryForUpdateDto Model): IRequest<OneOf<bool, ResponseException>>;
}
