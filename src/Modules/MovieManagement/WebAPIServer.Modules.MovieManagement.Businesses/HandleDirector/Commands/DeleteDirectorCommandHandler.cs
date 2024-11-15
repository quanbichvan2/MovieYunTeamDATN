using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Commands
{
    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, OneOf<bool, ResponseException>>
    {
        private readonly ILogger<DeleteDirectorCommandHandler> _logger;
        private readonly IDirectorRepository _directorRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDirectorCommandHandler(ILogger<DeleteDirectorCommandHandler> logger,
            IDirectorRepository directorRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _directorRepository = directorRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var director = await _directorRepository.FindByIdAsync(request.Id);
                if (director is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.NotFound);
                }
                _directorRepository.Delete(director);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.OperationFailed, ex.ToString());
            }
        }
    }
}
