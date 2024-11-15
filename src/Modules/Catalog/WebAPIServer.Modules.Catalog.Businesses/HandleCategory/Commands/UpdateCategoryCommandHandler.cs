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
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateCategoryCommandHandler> _logger;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IValidator<CategoryForUpdateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public UpdateCategoryCommandHandler(IMapper mapper,
			ILogger<UpdateCategoryCommandHandler> logger,
			ICategoryRepository categoryRepository,
			IValidator<CategoryForUpdateDto> validator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_categoryRepository = categoryRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Category>(ErrorCode.UpdateError, validationResult.Errors);
				}
				var category = await _categoryRepository.FindByIdAsync(request.Id);
				if (category != null)
				{
					var productForUpdate = await _categoryRepository.IsNameExistsAsyncForUpdate(category.Name, request.Id);
					if (productForUpdate == true)
					{
						return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.UpdateError, validationResult.Errors);
					}
					category.CreatedAt = DateTime.UtcNow;
					_mapper.Map(request, category);
					_categoryRepository.Update(category);
					await _unitOfWork.SaveChangesAsync();
					return true;
				}

				return ResponseExceptionHelper.ErrorResponse<Category>(ErrorCode.NotFound, validationResult.Errors);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw new NullReferenceException(nameof(Handle));
			}
		}
	}
}
