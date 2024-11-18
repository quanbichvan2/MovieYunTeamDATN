using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Services;
using WebAPIServer.Modules.Identity.Businesses.Models;

namespace WebAPIServer.Modules.Identity.Api.Controllers
{
	internal class IdentityController : BaseController
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly ITokenService _tokenService;
        private IMemoryCache _cache;
        public IdentityController(IAuthenticationService authenticationService,
            ITokenService tokenService,
            IMemoryCache cache)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
            _cache = cache;
        }
        [HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			var response = await _authenticationService.Login(model);
			return response.Match<IActionResult>(
				res =>
				{
					SetTokensInCookies(res.Token!);
					return Ok(res);
				},
				error => BadRequest(response.AsT1));
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto model, CancellationToken cancellationToken)
		{
			var response = await _authenticationService.Register(model, cancellationToken);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword()
        {
            string emailClaim = User.Claims.First(c => c.Type == ClaimTypes.Email)?.Value;
            var response = await _authenticationService.ResetPassword(emailClaim);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

        [HttpPost("confirm-otp")]
        public async Task<IActionResult> ConfirmOTP(string otp)
        {
            string emailClaim = User.Claims.First(c => c.Type == ClaimTypes.Email)?.Value;
            var cacheKey = new { email = emailClaim, otp = otp };
            if (_cache.TryGetValue(cacheKey, out _))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken()
		{
			if (Request.Cookies.TryGetValue("refresh_token", out var refreshToken))
			{
				// Kiểm tra refreshToken hợp lệ (ví dụ: kiểm tra trong cơ sở dữ liệu)
				var user = await _authenticationService.ValidateRefreshToken(refreshToken);
				if (user == null) return Unauthorized();

				// Tạo Access Token mới
				var newAccessToken = await _tokenService.GenerateAccessToken(user);

				// Cập nhật Access Token trong cookie
				Response.Cookies.Append("access_token", newAccessToken, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.Strict,
					Expires = DateTimeOffset.UtcNow.AddMinutes(15)
				});

				return Ok(new { message = "Token refreshed" });
			}

			return Unauthorized();
		}

		private void SetTokensInCookies(string accessToken)
		{
			var refreshToken = _tokenService.GenerateRefreshToken();

			Response.Cookies.Append("access_token", accessToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddMinutes(15)
			});

			Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddDays(7) // Refresh Token có thời gian sống lâu hơn
			});
		}
	}
}
