using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Commands
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, OneOf<bool, ResponseException>>
	{
		private readonly ILogger<DeleteUserCommandHandler> _logger;
		private readonly IUserReponsitory _userRepository;
		private readonly IUnitOfWork _unitOfWork;
		public DeleteUserCommandHandler(
			ILogger<DeleteUserCommandHandler> logger,
			IUserReponsitory userRepository,
			IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userRepository.FindByIdAsync(request.Id);
				if (user == null)
				{
					return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.NotFound);
				}
				_userRepository.Delete(user);
				await _unitOfWork.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw new NullReferenceException(nameof(Handle));
			}
		}
	}
}
