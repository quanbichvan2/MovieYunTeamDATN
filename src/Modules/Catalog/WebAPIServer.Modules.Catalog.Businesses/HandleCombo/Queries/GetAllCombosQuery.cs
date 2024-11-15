using MediatR;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Queries
{
    public record GetAllCombosQuery(Filter filter): IRequest<PaginatedList<ComboForViewDto>>{ }
}
