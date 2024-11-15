using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Commands
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateProductCommandHandler> _logger;
		private readonly IProductRepository _productRepository;
		private readonly IValidator<ProductForUpdateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public UpdateProductCommandHandler(IMapper mapper,
			ILogger<UpdateProductCommandHandler> logger,
			IProductRepository productRepository,
			IValidator<ProductForUpdateDto> validator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_productRepository = productRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.UpdateError, validationResult.Errors);
				}

				Product? product = await _productRepository.FindByIdAsync(request.Id);
				if (product != null)
				{
					var productForUpdate = await _productRepository.IsNameExistsAsyncForUpdate(product.Name, request.Id);
					if (productForUpdate == true)
					{
						return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.UpdateError, validationResult.Errors);
					}
					product.ModifiedAt = DateTime.UtcNow;
					_mapper.Map(request.Model, product);
					_productRepository.Update(product);
					await _unitOfWork.SaveChangesAsync();
					return true;
				}
				return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.NotFound);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw new NullReferenceException(nameof(Handle));
			}
		}
	}
}
