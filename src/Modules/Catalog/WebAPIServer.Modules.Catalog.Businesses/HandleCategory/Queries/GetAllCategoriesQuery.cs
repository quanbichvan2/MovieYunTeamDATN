using MediatR;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Queries
{
    public record GetAllCategoriesQuery(Filter filter) : IRequest<PaginatedList<CategoryForViewDto>>;
}
    
