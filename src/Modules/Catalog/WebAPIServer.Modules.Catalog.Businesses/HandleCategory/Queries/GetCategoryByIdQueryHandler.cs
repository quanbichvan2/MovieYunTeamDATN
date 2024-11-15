using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, OneOf<CategoryForViewDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;
        public GetCategoryByIdQueryHandler(
            IMapper mapper, 
            ICategoryRepository categoryRepository, 
            ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<OneOf<CategoryForViewDto, ResponseException>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(request.id);
                if (category is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.NotFound);
                }
                return _mapper.Map<CategoryForViewDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.OperationFailed);
            }
        }
    }
}
