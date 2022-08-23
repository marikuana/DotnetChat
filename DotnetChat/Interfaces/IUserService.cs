using DotnetChat.Data.Models;
using System.Security.Claims;

namespace DotnetChat
{
    public interface IUserService
    {
        Task Authorize(User user, HttpContext httpContext);
        int GetUserId(ClaimsPrincipal principal);
    }
}