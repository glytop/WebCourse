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
            if (context.User.Identity.IsAuthenticated)
            {
                var isBlocked = context.User.Claims.FirstOrDefault(c => c.Type == "IsBlocked")?.Value;
                var path = context.Request.Path.Value?.ToLower();

                if (isBlocked == "True" &&
                    !(path.StartsWith("/auth/login") || path.StartsWith("/auth/logout") || path.StartsWith("/auth/forgotpassword")))
                {
                    context.Response.Redirect("/Auth/Login?blocked=true");
                    return;
                }
            }

            await _next(context);
        }
    }
}
