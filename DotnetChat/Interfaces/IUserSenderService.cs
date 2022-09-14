using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Controllers
{
    public interface IUserSenderService
    {
        void Send(IEnumerable<int> userIds, Action<IClientProxy> action);
    }
}