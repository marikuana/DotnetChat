using DotnetChat.Models;
using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Extensions
{
    public static class ChatHubExtensions
    {
        public static Task SendMessage(this IClientProxy clientProxy , MessageViewModel messageView)
        {
            return clientProxy.SendAsync("OnMessage", messageView);
        }
    }
}
