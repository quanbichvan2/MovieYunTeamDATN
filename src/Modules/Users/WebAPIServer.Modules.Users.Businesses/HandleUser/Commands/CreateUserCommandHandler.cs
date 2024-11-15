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
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<Guid, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CreateUserCommandHandler> _logger;
		private readonly IUserReponsitory _UserRepository;
		private readonly IValidator<UserForCreateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public CreateUserCommandHandler(
			IMapper mapper,
			ILogger<CreateUserCommandHandler> logger,
			IUserReponsitory userRepository,
			IUnitOfWork unitOfWork,
			IValidator<UserForCreateDto> validator)
		{
			_mapper = mapper;
			_logger = logger;
			_UserRepository = userRepository;
			_unitOfWork = unitOfWork;
			_validator = validator;
		}

		public async Task<OneOf<Guid, ResponseException>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.CreateError, validationResult.Errors);
				}
				User user = _mapper.Map<User>(request.Model);
				user.CreatedAt = DateTime.UtcNow;
				user.ModifiedAt = DateTime.UtcNow;
				await _UserRepository.CreateAsync(user);
				await _unitOfWork.SaveChangesAsync();
				return user.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.OperationFailed);
			}
		}
	}
}
