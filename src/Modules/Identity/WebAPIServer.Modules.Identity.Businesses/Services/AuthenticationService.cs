using FluentValidation;
using Microsoft.Extensions.Logging;
using OneOf;
using System.Net.Mail;
using System.Net;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Services;
using WebAPIServer.Modules.Identity.Businesses.Models;
using WebAPIServer.Modules.Identity.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace WebAPIServer.Modules.Identity.Businesses.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IAuthenticationRepository _authenticationRepository;
		private readonly ITokenService _tokenService;
		private readonly IValidator<LoginDto> _loginDtoValidator;
		private readonly IValidator<RegisterDto> _registerDtoValidator;
		private readonly ILogger<AuthenticationService> _logger;
        private IMemoryCache _cache;

        public AuthenticationService(IAuthenticationRepository authenticationRepository,
            ITokenService tokenService,
            IValidator<LoginDto> loginDtoValidator,
            IValidator<RegisterDto> registerDtoValidator,
            ILogger<AuthenticationService> logger,
            IMemoryCache cache)
        {
            _authenticationRepository = authenticationRepository;
            _tokenService = tokenService;
            _loginDtoValidator = loginDtoValidator;
            _registerDtoValidator = registerDtoValidator;
            _logger = logger;
            _cache = cache;
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

        async Task<OneOf<bool, ResponseException>> IAuthenticationService.ResetPassword(string emailClaim)
        {
            string fromEmail = "email@gmail.com";
            string toEmail = "email@gmail.com";
            string appPassword = "";
            string otp = GenerateRandomNumber(6);
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = "Mã xác minh cho email khôi phục: " + otp;
                mail.Body = "<h3>Xác minh email khôi phục của bạn</h3>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(fromEmail, appPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            var cacheKey = new { email = emailClaim, otp = otp };
            if (!_cache.TryGetValue(cacheKey, out _))
            {
                _cache.Set(cacheKey, otp, TimeSpan.FromMinutes(1));
            }
			else
			{
				throw new InvalidOperationException("Co lôĩ xảy ra!");
            }

            try
            {
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }

		private string GenerateRandomNumber(int length)
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += random.Next(0, 10).ToString();
            }

            return result;
        }
    }
}
