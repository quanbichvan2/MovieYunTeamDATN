using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Queries
{
    public class GetAllCombosQueryHandler : IRequestHandler<GetAllCombosQuery, PaginatedList<ComboForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IComboRepository _comboRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetAllCombosQueryHandler(IMapper mapper, 
            IComboRepository comboRepository, 
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _comboRepository = comboRepository;
            _logger = logger;
        }
        public async Task<PaginatedList<ComboForViewDto>> Handle(GetAllCombosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _comboRepository.GetAll();
                var allowedProductProperties = new List<string> { "Name", "Price", "Code" };
                query = query.SortBy(request.filter?.SortColumn, allowedProductProperties, request.filter.IsDescending);

                var paginatedProducts = await PaginatedList<Combo>.CreateAsync(
                    query,
                    request.filter.PageIndex,
                    request.filter.PageSize,
                    cancellationToken);

                var productViewDtos = _mapper.Map<List<ComboForViewDto>>(paginatedProducts.Items);

                var paginatedProductViews = new PaginatedList<ComboForViewDto>(
                    productViewDtos,
                    request.filter.PageIndex,
                    request.filter.PageSize,
                    paginatedProducts.TotalCount);
                return paginatedProductViews;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error fetching combos: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
