using AutoMapper;
using DotnetChat.Data;
using DotnetChat.Data.Models;
using DotnetChat.Extensions;
using DotnetChat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DotnetChat.Controllers
{

    [Authorize]
    public class ChatApiController : Controller
    {
        private IUserService userService;
        private IUserManager userManager;
        private IChatManager chatManager;
        private IMessageManager messageManager;
        private IMapper mapper;
        private IUserSenderService userSenderService;

        public ChatApiController(IChatManager chatManager, IUserManager userManager, IMapper mapper, IUserService userService, IMessageManager messageManager, IUserSenderService userSenderService)
        {
            this.chatManager = chatManager;
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
            this.messageManager = messageManager;
            this.userSenderService = userSenderService;
        }

        private User? GetUser()
        {
            return userManager.GetUser(userService.GetUserId(User));
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] SendMessageModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Chat? chat = chatManager.GetChat(model.ChatId);
            if (chat == null)
                return BadRequest();

            User? user = GetUser();
            if (user == null)
                return BadRequest();

            if (!userManager.HasAccess(user, chat))
                return Forbid();

            Message message = new Message()
            {
                Chat = chat,
                Author = user,
                Text = model.Text,
                CreatedDate = DateTime.UtcNow,
                MessageReplyId = model.ReplyMessage
            };
            messageManager.CreateMessage(message);

            MessageViewModel messageView = mapper.Map<MessageViewModel>(message);
            var userIds = chatManager.GetMembersId(chat);
            userSenderService.Send(userIds, c => c.SendMessage(messageView));

            return Ok();
        }

        [HttpGet]
        public IActionResult GetMessages(GetMessageModel getMessage)
        {
            User? user = GetUser();
            if (user == null)
                return BadRequest();

            Chat? chat = chatManager.GetChat(getMessage.ChatId);
            if (chat == null)
                return BadRequest();

            if (!userManager.HasAccess(user, chat))
                return Forbid();

            var messages = messageManager.GetMessages(chat, user, getMessage.LastMessageId, getMessage.Count);
            
            return Ok(mapper.Map<IEnumerable<MessageViewModel>>(messages));
        }

        [HttpPut]
        public IActionResult EditMessage(EditMessageModel editMessage)
        {
            User? user = GetUser();
            if (user == null)
                return BadRequest();

            Message? message = messageManager.GetMessage(editMessage.MessageId);
            if (message == null)
                return BadRequest();

            if (!messageManager.IsAuthor(message, user))
                return Forbid();

            message.Text = editMessage.NewText;
            messageManager.UpdateMessage(message);

            EditMessageViewModel messageView = mapper.Map<EditMessageViewModel>(message);
            var userIds = chatManager.GetMembersId(messageManager.GetMessageChat(message));
            userSenderService.Send(userIds, c => c.EditMessage(messageView));

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMessage(DeleteMessageModel deleteMessage)
        {
            User? user = GetUser();
            if (user == null)
                return BadRequest();

            Message? message = messageManager.GetMessage(deleteMessage.MessageId);
            if (message == null)
                return BadRequest();

            if (!messageManager.IsAuthor(message, user))
                return Forbid();

            message.Delete = deleteMessage.DeleteForMe ? Enums.MessageDelete.DeleteForMe : Enums.MessageDelete.DeleteForAll;
            messageManager.UpdateMessage(message);

            IEnumerable<int> usersId;
            if (message.Delete == Enums.MessageDelete.DeleteForMe)
            {
                usersId = new List<int>() { user.Id };
            }
            else
            {
                Chat chat = messageManager.GetMessageChat(message);
                usersId = new List<int>(chatManager.GetMembersId(chat));
            }

            DeleteMessageViewModel messageView = mapper.Map<DeleteMessageViewModel>(message);
            userSenderService.Send(usersId, c => c.DeleteMessage(messageView));

            return Ok();
        }
    }
}
