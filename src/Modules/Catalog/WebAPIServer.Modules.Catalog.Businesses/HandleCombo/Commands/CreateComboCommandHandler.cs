using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Commands
{
    public class CreateComboCommandHandler : IRequestHandler<CreateComboCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateComboCommandHandler> _logger;
        private readonly IComboRepository _comboRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<ComboForCreateDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateComboCommandHandler(IMapper mapper,
            ILogger<CreateComboCommandHandler> logger,
            IComboRepository comboRepository,
            IValidator<ComboForCreateDto> validator,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _comboRepository = comboRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<OneOf<Guid, ResponseException>> Handle(CreateComboCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.CreateError, validationResult.Errors);
                }

                /*bool isComboExisted = await _comboRepository.IsUniqueComboName(request.model.Name);
                if (!isComboExisted)
                {
                    return ResponseJwtTokenHelper.ErrorResponse<Combo>(ErrorCode.Existed);
                }*/

                Combo combo = _mapper.Map<Combo>(request.model);
                foreach (var productDto in combo.Products)
                {
                    var product = await _productRepository.FindByIdAsync(productDto.ProductId);
                    if (product == null)
                    {
                        return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.NotFound);
                    }
                    productDto.ProductName = product.Name;
                    productDto.ProductImage = product.Image!;
                    productDto.UnitPrice = product.Price;
                    bool isProductInCombo = await _comboRepository.IsProductComboExist(combo.Id, productDto?.ProductId);
                    if (isProductInCombo)
                    {
                        return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.Existed);
                    }
                }
                await _comboRepository.CreateAsync(combo);
                await _unitOfWork.SaveChangesAsync();

                return combo.Id;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.OperationFailed);
            }
        }
    }
}
