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
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CreateProductCommandHandler> _logger;
		private readonly IProductRepository _productRepository;
		private readonly IValidator<ProductForCreateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public CreateProductCommandHandler(
			IMapper mapper,
			IValidator<ProductForCreateDto> validator,
			IProductRepository productRepository,
			IUnitOfWork unitOfWork,
			ILogger<CreateProductCommandHandler> logger)
		{
			_mapper = mapper;
			_validator = validator;
			_productRepository = productRepository;
			_unitOfWork = unitOfWork;
			_logger = logger;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.CreateError, validationResult.Errors);
				}
				Product product = _mapper.Map<Product>(request.Model);
				await _productRepository.CreateAsync(product);
				await _unitOfWork.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.OperationFailed);
			}
		}
	}
}