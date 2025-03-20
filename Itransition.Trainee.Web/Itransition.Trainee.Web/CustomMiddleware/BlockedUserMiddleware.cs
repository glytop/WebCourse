using Itransition.Trainee.Web.Services;
using Microsoft.AspNetCore.Authentication;

namespace Itransition.Trainee.Web.CustomMiddleware
{
    public class BlockedUserMiddleware
    {
        private RequestDelegate _next;

        public BlockedUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (path.StartsWith("/auth/login") || path.StartsWith("/auth/logout"))
            {
                await _next(context);
                return;
            }

            if (context.User.Identity.IsAuthenticated)
            {
                var isBlocked = context.User.Claims.FirstOrDefault(c => c.Type == "IsBlocked")?.Value;
                if (isBlocked == "True")
                {
                    await context.SignOutAsync(AuthService.AUTH_TYPE_KEY);
                    context.Response.Redirect("/Auth/Login?blocked=true");
                    return;
                }
            }

            await _next(context);
        }
    }
}
