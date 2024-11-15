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
    public class CreateCastMemberCommandHandler : IRequestHandler<CreateCastMemberCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCastMemberCommandHandler> _logger;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CastMemberForCreateDto> _validator;
        public CreateCastMemberCommandHandler(IMapper mapper,
            ILogger<CreateCastMemberCommandHandler> logger,
            ICastMemberRepository castMemberRepository,
            IUnitOfWork unitOfWork,
            IValidator<CastMemberForCreateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _castMemberRepository = castMemberRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<OneOf<Guid, ResponseException>> Handle(CreateCastMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.CreateError, validationResult.Errors);
                }
                CastMember castMember = _mapper.Map<CastMember>(request.Model);
                await _castMemberRepository.CreateAsync(castMember);
                await _unitOfWork.SaveChangesAsync();
                return castMember.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.OperationFailed);
            }
        }
    }
}