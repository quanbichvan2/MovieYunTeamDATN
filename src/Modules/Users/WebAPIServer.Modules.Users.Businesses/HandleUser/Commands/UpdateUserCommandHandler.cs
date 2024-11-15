using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Modules.Users.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Commands
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IUserReponsitory _userRepository;
        private readonly IValidator<UserForUpdateDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
		public UpdateUserCommandHandler(
			IMapper mapper,
			ILogger<UpdateUserCommandHandler> logger,
			IUserReponsitory userReponsitory,
			IUnitOfWork unitOfWork,
			IValidator<UserForUpdateDto> validator)
		{
			_mapper = mapper;
			_logger = logger;
			_userRepository = userReponsitory;
			_unitOfWork = unitOfWork;
			_validator = validator;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.UpdateError, validationResult.Errors);
                }

                var user = await _userRepository.FindByIdAsync(request.Id);
                if (user != null)
                {
                    var userForUpdate = await _userRepository.IsEmailExistsAsync(user.Name, request.Id);
                    if (userForUpdate == true)
                    {
                        return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.Existed);
                    }
                    user.ModifiedAt = DateTime.UtcNow;
                    _mapper.Map(request.Model, user);
                    _userRepository.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
