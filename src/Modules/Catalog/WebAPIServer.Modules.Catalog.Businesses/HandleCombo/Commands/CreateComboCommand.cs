using MediatR;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Commands
{
    public record CreateComboCommand(ComboForCreateDto model) : IRequest<OneOf<Guid, ResponseException>>;
}
