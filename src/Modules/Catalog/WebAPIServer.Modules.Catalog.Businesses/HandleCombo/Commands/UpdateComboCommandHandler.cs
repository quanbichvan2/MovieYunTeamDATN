using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Commands
{
	public class UpdateComboCommandHandler : IRequestHandler<UpdateComboCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateComboCommandHandler> _logger;
        private readonly IComboRepository _comboRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<ComboForUpdateDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateComboCommandHandler(IMapper mapper,
            ILogger<UpdateComboCommandHandler> logger,
            IComboRepository comboRepository,
            IValidator<ComboForUpdateDto> validator,
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

        public async Task<OneOf<bool, ResponseException>> Handle(UpdateComboCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.UpdateError, validationResult.Errors);
                }
                var combo = await _comboRepository.FindByIdAsync(request.id);
                if (combo == null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.NotFound, validationResult.Errors);
                }
                if (request.model.Name != combo.Name)
                {
                    var isComboExisted = _comboRepository.GetAll().Any(x => x.Name == request.model.Name);
                    if (isComboExisted)
                    {
                        return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.Existed);
                    }
                }
                combo.Name = request.model.Name!;
                combo.Description = request.model.Description;
                combo.Image = request.model.Image;
                combo.Price = request.model.Price;

                var existingProducts = combo.Products.ToList();
                var updatedProducts = request.model.Products;

                foreach (var existingProduct in existingProducts)
                {
                    if (!updatedProducts.Any(up => up.Id == existingProduct.ProductId))
                    {
                        _unitOfWork.Entry(existingProduct, EntityState.Deleted);
                    }
                }
                foreach (var productDto in updatedProducts)
                {
                    var product = await _productRepository.FindByIdAsync(productDto.Id);
                    if (product == null)
                    {
                        _logger.LogWarning("Product with ID {Id} not found.", productDto.Id);
                        return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.NotFound);
                    }

                    var existingProduct = combo.Products.FirstOrDefault(p => p.ProductId == productDto.Id);
                    if (existingProduct != null)
                    {
                        existingProduct.Quantity = productDto.Quantity;
                        existingProduct.UnitPrice = product.Price;
                        existingProduct.ProductName = product.Name;
                        existingProduct.ProductImage = product.Image!;
                        _unitOfWork.Entry(existingProduct, EntityState.Modified);
                    }
                    else
                    {
                        var newProductCombo = new ComboProduct
                        {
                            Id = Guid.NewGuid(),
                            ProductId = productDto.Id,
                            Quantity = productDto.Quantity,
                            UnitPrice = product.Price,
                            ProductName = product.Name,
                            ProductImage = product.Image!,
                            ComboId = combo.Id
                        };
                        _unitOfWork.Entry(newProductCombo, EntityState.Added);
                    }
                }
                _unitOfWork.Entry(combo, EntityState.Modified);
                _comboRepository.Update(combo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error concurrency conflict detected.");
                return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.OperationFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating combo.");
                return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.OperationFailed);
            }
        }
    }
}