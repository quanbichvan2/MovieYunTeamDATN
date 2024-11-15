using MediatR;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Queries
{
    public record GetComboByIdQuery(Guid id) : IRequest<OneOf<ComboForViewDetailsDto, ResponseException>>;
}
