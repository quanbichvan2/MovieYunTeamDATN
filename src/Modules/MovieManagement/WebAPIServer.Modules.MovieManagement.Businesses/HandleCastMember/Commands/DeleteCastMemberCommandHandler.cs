using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Commands
{
    public class DeleteCastMemberCommandHandler : IRequestHandler<DeleteCastMemberCommand, OneOf<bool, ResponseException>>
    {
        private readonly ILogger<DeleteCastMemberCommandHandler> _logger;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCastMemberCommandHandler(ILogger<DeleteCastMemberCommandHandler> logger,
            ICastMemberRepository castMemberRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _castMemberRepository = castMemberRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(DeleteCastMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var castMember = await _castMemberRepository.FindByIdAsync(request.Id);
                if (castMember is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.NotFound);
                }
                _castMemberRepository.Delete(castMember);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.OperationFailed, ex.ToString());
            }
        }
    }
}
