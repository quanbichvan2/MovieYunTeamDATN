using MediatR;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries
{
    public record GetAllProductsQuery(Filter filter): IRequest<PaginatedList<ProductForViewDto>>;
}