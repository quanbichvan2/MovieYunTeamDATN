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
    public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDirectorCommandHandler> _logger;
        private readonly IDirectorRepository _directorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DirectorForUpdateDto> _validator;
        public UpdateDirectorCommandHandler(IMapper mapper, 
            ILogger<UpdateDirectorCommandHandler> logger, 
            IDirectorRepository directorRepository, 
            IUnitOfWork unitOfWork, 
            IValidator<DirectorForUpdateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _directorRepository = directorRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.UpdateError, validationResult.Errors);
                }
                var director = await _directorRepository.FindByIdAsync(request.Id);
                if (director is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.NotFound);
                }
                _mapper.Map(request.Model, director);
                _directorRepository.Update(director);
                await _unitOfWork.SaveChangesAsync();
                return true;
			}
			catch (Exception ex)
			{
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.OperationFailed);
            }
        }
    }
}