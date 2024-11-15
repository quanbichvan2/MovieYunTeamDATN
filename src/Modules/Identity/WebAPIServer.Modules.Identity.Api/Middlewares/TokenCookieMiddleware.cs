using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebAPIServer.Modules.Identity.Api.Middlewares
{
	public class TokenCookieMiddleware
	{
		private readonly RequestDelegate _next;

		public TokenCookieMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("access_token", out var token))
			{
				context.Request.Headers.Append("Authorization", $"Bearer {token}");
			}

			await _next(context);
		}
	}
}
