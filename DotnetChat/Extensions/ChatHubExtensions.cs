using DotnetChat.Models;
using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Extensions
{
    public static class ChatHubExtensions
    {
        public static Task SendMessage(this IClientProxy clientProxy, MessageViewModel messageView)
        {
            return clientProxy.SendAsync("OnMessage", messageView);
        }

        public static Task EditMessage(this IClientProxy clientProxy, EditMessageViewModel editMessageView)
        {
            return clientProxy.SendAsync("EditMessage", editMessageView);
        }

        public static Task DeleteMessage(this IClientProxy clientProxy, DeleteMessageViewModel deleteMessageView)
        {
            return clientProxy.SendAsync("DeleteMessage", deleteMessageView);
        }
    }
}
