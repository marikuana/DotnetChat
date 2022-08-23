using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Controllers
{
    [Authorize]
    public class ChatHub : Hub<ChatHub>
    {
        private UserManager userManager;
        private IUserConnertions userConnertions;

        public ChatHub(UserManager userManager, IUserConnertions userConnertions)
        {
            this.userManager = userManager;
            this.userConnertions = userConnertions;
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User == null)
                throw new Exception();

            int id = userManager.GetUserId(Context.User);
            string connectionId = Context.ConnectionId;
               
            userConnertions.UserConnection(id, connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User == null)
                throw new Exception();

            int id = userManager.GetUserId(Context.User);
            string connectionId = Context.ConnectionId;

            userConnertions.UserConnectionClose(id, connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
