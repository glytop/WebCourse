using Itransition.Trainee.Web.Attributes;
using Itransition.Trainee.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itransition.Trainee.Web.Controllers
{
    public class TableController : BaseController
    {
        public TableController(AuthService authService) : base(authService)
        {
        }

        [IsAuthenticated]
        public IActionResult Activity()
        {
            return View();
        }
    }
}
