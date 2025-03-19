using Itransition.Trainee.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Itransition.Trainee.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly AuthService _authService;

        public BaseController(AuthService authService)
        {
            _authService = authService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_authService.IsAuthenticated() || _authService.IsBlocked())
            {
                context.Result = RedirectToAction("Login", "Auth");
            }
            base.OnActionExecuting(context);
        }
    }
}
