using Itransition.Trainee.Web.Attributes;
using Itransition.Trainee.Web.Data.Repositories;
using Itransition.Trainee.Web.Models;
using Itransition.Trainee.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Itransition.Trainee.Web.Controllers
{
    public class TableController : Controller
    {
        private IUserRepositoryReal _userRepository;
        public TableController(IUserRepositoryReal userRepository)
        {
            _userRepository = userRepository;
        }

        [IsAuthenticated]
        public IActionResult Activity()
        {
            var users = _userRepository.GetAll()
                .OrderByDescending(u => u.LastLoginTime)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsBlocked = u.IsBlocked,
                    LastLoginTime = u.LastLoginTime,
                    CreatedAt = u.CreatedAt
                }).ToList();

            return View(users);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Block(List<Guid> id)
        {
            _userRepository.BlockUsers(id);
            return RedirectToAction("Activity");
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Unblock(List<Guid> id)
        {
            _userRepository.UnblockUsers(id);
            return RedirectToAction("Activity");
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Delete(List<Guid> id)
        {
            _userRepository.DeleteUsers(id);
            return RedirectToAction("Activity");
        }
    }
}
