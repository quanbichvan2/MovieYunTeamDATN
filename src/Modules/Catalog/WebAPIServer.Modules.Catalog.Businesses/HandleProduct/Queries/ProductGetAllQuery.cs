using MediatR;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries
{
    public record ProductGetAllQuery(Filter filter): IRequest<PaginatedList<ProductForViewDto>>;
}