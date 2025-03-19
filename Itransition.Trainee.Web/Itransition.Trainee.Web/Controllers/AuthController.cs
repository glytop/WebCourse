using Itransition.Trainee.Web.Data.Models;
using Itransition.Trainee.Web.Data.Repositories;
using Itransition.Trainee.Web.Models;
using Itransition.Trainee.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Itransition.Trainee.Web.Controllers
{
    public class AuthController : BaseController
    {
        private IUserRepositoryReal _userRepository;

        public AuthController(IUserRepositoryReal userRepositoryReal, AuthService authService) : base(authService)
        {
            _userRepository = userRepositoryReal;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = _userRepository.GetByEmail(viewModel.Email);
            if (user == null || user.Password != viewModel.Password)
            {
                ModelState.AddModelError("", "Invalid email or password");
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
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View(model);
            }

            var existingUser = _userRepository.GetByEmail(model.Email);
            if (existingUser is not null)
            {
                ModelState.AddModelError("", "User with this email already exists");
                return View(model);
            }

            var user = new UserData
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                IsBlocked = false,
                LastLoginTime = DateTime.UtcNow
            };

            _userRepository.Add(user);

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
