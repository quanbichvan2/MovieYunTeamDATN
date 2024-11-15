using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedList<CategoryForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;
        public GetAllCategoriesQueryHandler(
            IMapper mapper,
            ICategoryRepository categoryRepository, 
            ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<PaginatedList<CategoryForViewDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _categoryRepository.GetAll();
                var allowedCategoryProperties = new List<string> { "Name","Code" };
				if (!string.IsNullOrEmpty(request.filter.SearchTerm))
				{
					string search = request.filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Contains(search));
				}
				query = query.SortBy(request.filter?.SortColumn, allowedCategoryProperties, request.filter.IsDescending);
				var paginatedCategory = await PaginatedList<Category>.CreateAsync(
                    query,
                    request.filter.PageIndex,
                    request.filter.PageSize,
                    cancellationToken);

                var categoryViewDtos = _mapper.Map<List<CategoryForViewDto>>(paginatedCategory.Items);

                var paginatedCategoryViews = new PaginatedList<CategoryForViewDto>(
                    categoryViewDtos,
                    request.filter.PageIndex,
                    request.filter.PageSize,
                    paginatedCategory.TotalCount);
                return paginatedCategoryViews;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error fetching products: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
