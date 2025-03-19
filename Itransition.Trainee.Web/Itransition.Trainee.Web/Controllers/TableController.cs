using Itransition.Trainee.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Itransition.Trainee.Web.Controllers
{
    public class TableController : Controller
    {
        [IsAuthenticated]
        public IActionResult Activity()
        {
            return View();
        }
    }
}
