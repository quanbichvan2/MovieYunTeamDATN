using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Commands
{
    public class UpdateCastMemberCommandHandler : IRequestHandler<UpdateCastMemberCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCastMemberCommandHandler> _logger;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CastMemberForUpdateDto> _validator;
        public UpdateCastMemberCommandHandler(IMapper mapper, 
            ILogger<UpdateCastMemberCommandHandler> logger, 
            ICastMemberRepository castMemberRepository, 
            IUnitOfWork unitOfWork, 
            IValidator<CastMemberForUpdateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _castMemberRepository = castMemberRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(UpdateCastMemberCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.UpdateError, validationResult.Errors);
                }
                var castMember = await _castMemberRepository.FindByIdAsync(request.Id);
                if (castMember is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.NotFound);
                }
                _mapper.Map(request.Model, castMember);
                _castMemberRepository.Update(castMember);
                await _unitOfWork.SaveChangesAsync();
                return true;
			}
			catch (Exception ex)
			{
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.OperationFailed);
            }
        }
    }
}