using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Commands
{
    public record DeleteComboCommand(Guid id) : IRequest<OneOf<bool, ResponseException>>;

}
