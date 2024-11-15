using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Commands
{
	public class DeleteComboCommandHandler : IRequestHandler<DeleteComboCommand, OneOf<bool, ResponseException>>
    {
        private readonly ILogger<UpdateComboCommandHandler> _logger;
        private readonly IComboRepository _comboRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteComboCommandHandler(ILogger<UpdateComboCommandHandler> logger,
            IComboRepository comboRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _comboRepository = comboRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(DeleteComboCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var combo = await _comboRepository.FindByIdAsync(request.id);
                if (combo == null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.NotFound);
                }
                _comboRepository.Delete(combo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.OperationFailed);
            }
        }
    }
}
