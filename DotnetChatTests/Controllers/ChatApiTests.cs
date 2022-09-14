using DotnetChat;
using DotnetChat.Controllers;
using DotnetChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;
using AutoMapper;
using DotnetChat.Mapper;
using DotnetChat.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.SignalR;

namespace DotnetChatTests
{
    public class ChatApiTests
    {
        [Fact]
        public void SendMessageReturnABadRequestObjectResultWhenANotValidModel()
        {
            var sendMessageModel = new SendMessageModel();
            var chatApi = new ChatApiController(null, null, null, null, null, null);
            chatApi.ModelState.AddModelError("Text", "Required");

            var result = chatApi.SendMessage(sendMessageModel);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void SendMessageReturnABadRequestResultWhenChatNotFound()
        {
            var sendMessageModel = new SendMessageModel() { ChatId = 0 };
            Mock<IChatManager> chatManagerMock = new Mock<IChatManager>();
            chatManagerMock.Setup(m => m.GetChat(sendMessageModel.ChatId)).Returns(null as Chat);
            var chatApi = new ChatApiController(chatManagerMock.Object, null, null, null, null, null);

            var result = chatApi.SendMessage(sendMessageModel);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void SendMessageReturnABadRequestResultWhenUserNotFound()
        {
            Chat chat = new Group() { Id = 1 };
            int userId = 0;
            var sendMessageModel = new SendMessageModel() { ChatId = chat.Id };
            var chatManager = new Mock<IChatManager>();
            chatManager.Setup(m => m.GetChat(chat.Id)).Returns(chat);
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            var userManager = new Mock<IUserManager>();
            userManager.Setup(x => x.GetUser(userId)).Returns(null as User);
            var chatApi = new ChatApiController(chatManager.Object, userManager.Object, null, userService.Object, null, null);

            var result = chatApi.SendMessage(sendMessageModel);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void SendMessageReturnAForbidResultWhenUserHasNotAccess()
        {
            Chat chat = new Group() { Id = 1 };
            User user = new User() { Id = 1 };
            var sendMessageModel = new SendMessageModel() { ChatId = chat.Id };
            var chatManager = new Mock<IChatManager>();
            chatManager.Setup(m => m.GetChat(chat.Id)).Returns(chat);
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(user.Id);
            var userManager = new Mock<IUserManager>();
            userManager.Setup(x => x.GetUser(user.Id)).Returns(user);
            userManager.Setup(x => x.HasAccess(It.IsAny<User>(), It.IsAny<Chat>())).Returns(false);
            var chatApi = new ChatApiController(chatManager.Object, userManager.Object, null, userService.Object, null, null);

            var result = chatApi.SendMessage(sendMessageModel);

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void SendMessageReturnAOkResultAndCreateMessage()
        {
            Chat chat = new Group() { Id = 1 };
            User user = new User() { Id = 1 };
            var sendMessageModel = new SendMessageModel() { ChatId = chat.Id };
            var chatManager = new Mock<IChatManager>();
            chatManager.Setup(m => m.GetChat(chat.Id)).Returns(chat);
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(user.Id);
            var userManager = new Mock<IUserManager>();
            userManager.Setup(x => x.GetUser(user.Id)).Returns(user);
            userManager.Setup(x => x.HasAccess(user, chat)).Returns(true);
            var messageManager = new Mock<IMessageManager>();
            var mapper = new MapperConfiguration(cfg => { 
                cfg.AddProfile<MessageProfile>(); 
                cfg.AddProfile<UserProfile>();
            }).CreateMapper();
            var userSenderService = new Mock<IUserSenderService>();
            userSenderService.Setup(x => x.Send(new List<int>() { user.Id }, It.IsAny<Action<IClientProxy>>()));
            var chatApi = new ChatApiController(chatManager.Object, userManager.Object, mapper, userService.Object, messageManager.Object, userSenderService.Object);

            var result = chatApi.SendMessage(sendMessageModel);

            Assert.IsType<OkResult>(result);
            messageManager.Verify(x => x.CreateMessage(It.IsAny<Message>()));
        }
    }
}