using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Controllers
{
    [Authorize]
    public class ChatHub : Hub<ChatHub>
    {
        private IUserConnertions userConnertions;
        private IUserService userService;

        public ChatHub(IUserService userService, IUserConnertions userConnertions)
        {
            this.userService = userService;
            this.userConnertions = userConnertions;
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User == null)
                throw new Exception();

            int id = userService.GetUserId(Context.User);
            string connectionId = Context.ConnectionId;
               
            userConnertions.UserConnection(id, connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User == null)
                throw new Exception();

            int id = userService.GetUserId(Context.User);
            string connectionId = Context.ConnectionId;

            userConnertions.UserConnectionClose(id, connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
