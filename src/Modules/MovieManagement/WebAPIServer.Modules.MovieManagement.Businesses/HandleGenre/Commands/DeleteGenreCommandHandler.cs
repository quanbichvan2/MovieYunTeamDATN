using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Commands
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, OneOf<bool, ResponseException>>
    {
        private readonly ILogger<DeleteGenreCommandHandler> _logger;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGenreCommandHandler(ILogger<DeleteGenreCommandHandler> logger,
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var genre = await _genreRepository.FindByIdAsync(request.id);
                if (genre is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.NotFound);
                }
                _genreRepository.Delete(genre);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.OperationFailed, ex.ToString());
            }
        }
    }
}
