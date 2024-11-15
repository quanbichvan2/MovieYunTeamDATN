using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Commands
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDirectorCommandHandler> _logger;
        private readonly IDirectorRepository _directorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DirectorForCreateDto> _validator;
        public CreateDirectorCommandHandler(IMapper mapper,
            ILogger<CreateDirectorCommandHandler> logger,
            IDirectorRepository directorRepository,
            IUnitOfWork unitOfWork,
            IValidator<DirectorForCreateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _directorRepository = directorRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<OneOf<Guid, ResponseException>> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.CreateError, validationResult.Errors);
                }
                Director director = _mapper.Map<Director>(request.Model);
                await _directorRepository.CreateAsync(director);
                await _unitOfWork.SaveChangesAsync();
                return director.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.OperationFailed);
            }
        }
    }
}