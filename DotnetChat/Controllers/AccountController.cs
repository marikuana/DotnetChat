using DotnetChat.Data;
using DotnetChat.Data.Models;
using DotnetChat.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DotnetChat.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository userRepository;
        private IUserService userService;

        public AccountController(IUserRepository userRepository, IUserService userService)
        {
            this.userRepository = userRepository;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User? user = userRepository.Get(u => u.Login == model.Login && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Account not found");
                return View(model);
            }

            await userService.Authorize(user, HttpContext);
            return RedirectToAction("Index", "Chat");
        }
    }
}
