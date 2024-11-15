using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        private readonly ICategoryRepository _categoryRespository;
        private readonly IValidator<CategoryForCreateDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IMapper mapper, 
            ILogger<CreateCategoryCommandHandler> logger, 
            ICategoryRepository categoryRespository, 
            IValidator<CategoryForCreateDto> validator, 
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _categoryRespository = categoryRespository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }


        public async Task<OneOf<bool, ResponseException>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.model);
            try
            {
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Category>(ErrorCode.CreateError, validationResult.Errors);
                }
                Category category = _mapper.Map<Category>(request.model);
                await _categoryRespository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Category>(ErrorCode.OperationFailed);
            }
        }
    }
}
