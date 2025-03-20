using Itransition.Trainee.Web.Data.Models;
using Itransition.Trainee.Web.Data.Repositories;
using Itransition.Trainee.Web.Models;
using Itransition.Trainee.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Itransition.Trainee.Web.Controllers
{
    public class AuthController : Controller
    {
        private IUserRepositoryReal _userRepository;

        public AuthController(IUserRepositoryReal userRepositoryReal)
        {
            _userRepository = userRepositoryReal;
        }

        [HttpGet]
        public IActionResult Login(bool blocked = false)
        {
            if (blocked)
            {
                ViewBag.BlockedMessage = "Your account is blocked.";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            var user = _userRepository.Login(viewModel.Email, viewModel.Password);

            if (user is null)
            {
                ModelState.AddModelError(
                    nameof(viewModel.Email),
                    "Не правильный логин или пароль");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (user.IsBlocked)
            {
                ModelState.AddModelError("", "Your account is blocked");
                return View(viewModel);
            }

            user.LastLoginTime = DateTime.UtcNow;
            _userRepository.Update(user);

            var claims = new List<Claim>
            {
                new Claim(AuthService.CLAIM_TYPE_ID, user.Id.ToString()),
                new Claim(AuthService.CLAIM_TYPE_NAME, user.Name),
                new Claim(AuthService.CLAIM_TYPE_IS_BLOCKED, user.IsBlocked.ToString()),
                new Claim (ClaimTypes.AuthenticationMethod, AuthService.AUTH_TYPE_KEY),

            };

            var identity = new ClaimsIdentity(claims, AuthService.AUTH_TYPE_KEY);

            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(AuthService.AUTH_TYPE_KEY, principal,
                new AuthenticationProperties
                {
                    IsPersistent = viewModel.RememberMe
                }).Wait();

            return RedirectToAction("Activity", "Table");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid || (viewModel.Password != viewModel.ConfirmPassword) || (!_userRepository.CheckIsEmailAvailable(viewModel.Email)))
            {
                return View(viewModel);
            }

            _userRepository.Register(viewModel.Name, viewModel.Email, viewModel.Password);

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext
                .SignOutAsync(AuthService.AUTH_TYPE_KEY)
                .Wait();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
