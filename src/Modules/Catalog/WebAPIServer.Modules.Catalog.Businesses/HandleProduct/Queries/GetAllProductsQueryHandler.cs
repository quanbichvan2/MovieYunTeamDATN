using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<ProductForViewDto>>
	{
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepository;
		private readonly ILogger<GetProductByIdQueryHandler> _logger;
		public GetAllProductsQueryHandler(
			IMapper mapper,
			IProductRepository productRepository,
			ILogger<GetProductByIdQueryHandler> logger)
		{
			_mapper = mapper;
			_productRepository = productRepository;
			_logger = logger;
		}
		public async Task<PaginatedList<ProductForViewDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var query = _productRepository.GetAll();
				var allowedProductProperties = new List<string> { "Name", "Price", "Code" };
				query = query.SortBy(request.filter?.SortColumn, allowedProductProperties, request.filter.IsDescending);

				var paginatedProducts = await PaginatedList<Product>.CreateAsync(
					query,
					request.filter.PageIndex,
					request.filter.PageSize,
					cancellationToken);

				var productViewDtos = _mapper.Map<List<ProductForViewDto>>(paginatedProducts.Items);

				var paginatedProductViews = new PaginatedList<ProductForViewDto>(
					productViewDtos,
					request.filter.PageIndex,
					request.filter.PageSize,
					paginatedProducts.TotalCount);
				return paginatedProductViews;
			}
			catch (Exception ex)
			{

				_logger.LogError($"Error fetching products: {ex.Message}");
				throw new NullReferenceException(nameof(Handle), ex);
			}
		}
	}
}
