using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, OneOf<ProductForViewDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(
            IMapper mapper,
            IProductRepository productRepository,
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<OneOf<ProductForViewDto, ResponseException>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.FindByIdAsync(request.id);
                if (product is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.NotFound);
                }
                return _mapper.Map<ProductForViewDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.OperationFailed);
            }
        }
    }
}