
using ProductionOrderERP_API.ERP.Core.Service;

namespace ProductionOrderERP_API.ERP.Infrastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public JwtMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var user = _tokenService.ValidateToken(token);
                if (user != null)
                {
                    context.Items["User"] = user; // Store user info in context for access in controllers
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized - Invalid or expired token");
                    return;
                }
            }

            await _next(context); // Proceed to the next middleware
        }
    }
}
