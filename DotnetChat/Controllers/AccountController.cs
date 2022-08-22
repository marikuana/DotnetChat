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

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("Id", user.Id.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, "Id");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            return RedirectToAction("Index", "Chat");
        }
    }
}
