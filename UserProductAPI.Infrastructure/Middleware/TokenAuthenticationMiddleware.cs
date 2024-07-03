using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserProductAPI.Infrastructure.Interface;

namespace UserProductAPI.Infrastructure.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenInterface tokenService)
        {
            var path = context.Request.Path.Value;
            if (path != null && (path.Contains("/api/User/register") || path.Contains("/api/User/login")))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header missing.");
                return;
            }

            var tokenString = token.ToString().Replace("Bearer ", "");
            if (!tokenService.ValidateToken(tokenString, out var user))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
                return;
            }

            context.Items["User"] = user;
            await _next(context);
        }
    }
}

