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

namespace DotnetChatTests
{
    public class ChatControllerTests
    {
        [Fact]
        public void IndexReturnANotFoundResult()
        {
            int id = 0;
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(id);
            var userManager = new Mock<IUserManager>();
            userManager.Setup(x => x.GetUser(id)).Returns(null as User);
            var chatController = new ChatController(userManager.Object, null, userService.Object, null);

            var result = chatController.Index();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void IndexReturnAViewResultWithIEnumerableChatViewModel()
        {
            IEnumerable<Chat> chats = new List<Chat>()
            {
                new Group() { Name = "Group" }
            };
            User user = new User() { Id = 1, Login = "Login", Password = "Pass", Chats = chats };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(user.Id);
            var userManager = new Mock<IUserManager>();
            userManager.Setup(x => x.GetUser(user.Id)).Returns(user);
            userManager.Setup(x => x.GetUserChats(It.IsAny<User>())).Returns(user.Chats);
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ChatProfile>());
            var mapper = mapperConfig.CreateMapper();

            var chatController = new ChatController(userManager.Object, mapper, userService.Object, null);

            var result = chatController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var chatViewModels = Assert.IsAssignableFrom<IEnumerable<ChatViewModel>>(viewResult.Model);
            Assert.Equal(chatViewModels.Count(), chats.Count());
        }
    }
}