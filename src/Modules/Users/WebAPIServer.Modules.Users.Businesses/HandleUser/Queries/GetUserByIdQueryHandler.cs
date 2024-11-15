using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Modules.Users.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Queries
{
	public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, OneOf<UserForViewDto, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly IUserReponsitory  _userRepository;
		private readonly ILogger<GetUserByIdQueryHandler> _logger;
		public GetUserByIdQueryHandler(IMapper mapper, 
			IUserReponsitory userRepository, 
			ILogger<GetUserByIdQueryHandler> logger)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_logger = logger;
		}

		public async Task<OneOf<UserForViewDto, ResponseException>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userRepository.FindByIdAsync(request.Id);
				if (user is null)
				{
					return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.NotFound);
				}
				return _mapper.Map<UserForViewDto>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<User>(ErrorCode.OperationFailed);
			}
		}
	}
}
