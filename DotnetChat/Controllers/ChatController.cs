using AutoMapper;
using DotnetChat.Data;
using DotnetChat.Data.Models;
using DotnetChat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetChat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private IUserManager userManager;
        private IMapper mapper;
        private IUserService userService;
        private IChatManager chatManager;

        public ChatController(IUserManager userManager, IMapper mapper, IUserService userService, IChatManager chatManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
            this.chatManager = chatManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int id = userService.GetUserId(User);
            User? user = userManager.GetUser(id);
            if (user == null)
                return NotFound();

            var chats = userManager.GetUserChats(user);

            foreach (var chat in chats) // REWORK
            {
                if (string.IsNullOrEmpty(chat.Name))
                    chat.Name = chatManager.GetChatName(chat, user);
            }

            IEnumerable<ChatViewModel> chatsViews = mapper.Map<IEnumerable<Chat>, IEnumerable<ChatViewModel>>(chats);
            return View(chatsViews);
        }
    }
}
