using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Controllers
{
    public class UserSenderService : IUserSenderService
    {
        private IHubContext<ChatHub> hubContext;
        private IOnlineUserService onlineUserService;

        public UserSenderService(IHubContext<ChatHub> hubContext, IOnlineUserService onlineUserService)
        {
            this.hubContext = hubContext;
            this.onlineUserService = onlineUserService;
        }

        public void Send(IEnumerable<int> userIds, Action<IClientProxy> action)
        {
            action.Invoke(hubContext.Clients.Clients(onlineUserService.GetUserConnections(userIds)));
        }
    }
}
