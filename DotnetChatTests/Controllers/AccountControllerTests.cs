using DotnetChat.Controllers;
using DotnetChat.Data;
using DotnetChat.Data.Models;
using DotnetChat.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace DotnetChatTests
{
    public class AccountControllerTests
    {       
        [Fact]
        public async void LoginReturnAViewResultWithLoginModel()
        {
            var accountController = new AccountController(null);
            var loginModel = new LoginModel();
            accountController.ModelState.AddModelError("Login", "Max Lenght 64");

            var result = await accountController.Login(loginModel);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(loginModel, viewResult.Model);
        }

        [Fact]
        public async void LoginReturnAViewResultWithLoginModelAndModelError()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.Get(u => true)).Returns(null as User);
            var accountController = new AccountController(userRepository.Object);
            var loginModel = new LoginModel();

            var result = await accountController.Login(loginModel);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(loginModel, viewResult.Model);
            Assert.NotEmpty(viewResult.ViewData.ModelState);
        }

        /*[Fact]
        public async void LoginReturnARedirectToActionResult()
        {
            var user = new User() { Login = "User", Password = "123" };
            var loginModel = new LoginModel() { Login = user.Login, Password = user.Password };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<Expression<Func<User,bool>>>())).Returns(user);
            var accountController = new AccountController(userRepository.Object);

            var result = await accountController.Login(loginModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Chat", redirectToActionResult.ControllerName);
        }*/
    }
}