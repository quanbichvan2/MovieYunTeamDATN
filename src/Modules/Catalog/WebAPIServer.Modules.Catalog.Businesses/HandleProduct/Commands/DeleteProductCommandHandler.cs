using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Commands
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, OneOf<bool, ResponseException>>
    {
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
		public DeleteProductCommandHandler(
			ILogger<DeleteProductCommandHandler> logger,
			IProductRepository productRepository,
			IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_productRepository = productRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.FindByIdAsync(request.Id);
                if (product == null)
                {
                        return ResponseExceptionHelper.ErrorResponse<Product>(ErrorCode.NotFound);
                }
                _productRepository.Delete(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
