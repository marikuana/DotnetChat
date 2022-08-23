using DotnetChat.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace DotnetChat
{
    public class UserService : IUserService
    {
        public int GetUserId(ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.FindFirstValue("Id"));
        }

        public Task Authorize(User user, HttpContext httpContext)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("Id", user.Id.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, "Id");
            return httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
