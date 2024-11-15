using FluentValidation;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Services;
using WebAPIServer.Modules.Identity.Businesses.Models;
using WebAPIServer.Modules.Identity.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Identity.Businesses.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IAuthenticationRepository _authenticationRepository;
		private readonly ITokenService _tokenService;
		private readonly IValidator<LoginDto> _loginDtoValidator;
		private readonly IValidator<RegisterDto> _registerDtoValidator;
		private readonly ILogger<AuthenticationService> _logger;

		public AuthenticationService(IAuthenticationRepository authenticationRepository,
			ITokenService tokenService,
			IValidator<LoginDto> loginDtoValidator,
			IValidator<RegisterDto> registerDtoValidator,
			ILogger<AuthenticationService> logger)
		{
			_authenticationRepository = authenticationRepository;
			_tokenService = tokenService;
			_loginDtoValidator = loginDtoValidator;
			_registerDtoValidator = registerDtoValidator;
			_logger = logger;
		}
		public async Task<OneOf<bool, ResponseException>> Register(RegisterDto modelDto, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _registerDtoValidator.ValidateAsync(modelDto);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<RegisterDto>(ErrorCode.CreateError, validationResult.Errors);
				}
				var hashedPassword = BCrypt.Net.BCrypt.HashPassword(modelDto.Password);

				var user = new UserIdentity
				{
					Id = Guid.NewGuid(),
					Email = modelDto.Email.ToLower(),
					PasswordHash = hashedPassword,
				};
				await _authenticationRepository.AddUserAsync(user);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<RegisterDto>(ErrorCode.OperationFailed);
			}
		}
		public async Task<OneOf<ResponseJwtHelper, ResponseException>> Login(LoginDto modelDto)
		{
			try
			{
				var validationResult = await _loginDtoValidator.ValidateAsync(modelDto);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<RegisterDto>(ErrorCode.CreateError, validationResult.Errors);
				}
				var user = await _authenticationRepository.GetUserByEmail(modelDto.Email);
				if (user == null)
				{
					return new ResponseJwtHelper
					{
						Message = "Email hoặc mật khẩu sai, vui lòng thử lại",
						IsSuccess = false,
					};
				}
				var result = !BCrypt.Net.BCrypt.Verify(modelDto.Password, user.PasswordHash);
				if (result)
				{
					return new ResponseJwtHelper
					{
						Message = "Email hoặc mật khẩu sai, vui lòng thử lại",
						IsSuccess = false,
					};
				}
				var accessToken = await _tokenService.GenerateAccessToken(user!);
				var refreshToken = _tokenService.GenerateRefreshToken();
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
				_authenticationRepository.Update(user);
				return new ResponseJwtHelper
				{
					Message = "Thành công",
					IsSuccess = true,
					Token = accessToken
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<LoginDto>(ErrorCode.OperationFailed);
			}
		}
		public async Task<UserIdentity> ValidateRefreshToken(string refreshToken)
		{
			var user = await _authenticationRepository.GetUserByRefreshTokenAsync(refreshToken);
			return user;
		}
	}
}
