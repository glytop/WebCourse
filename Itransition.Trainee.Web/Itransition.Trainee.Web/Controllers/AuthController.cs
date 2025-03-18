using Microsoft.AspNetCore.Mvc;

namespace Itransition.Trainee.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
