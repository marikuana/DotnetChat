using DotnetChat.Controllers;
using DotnetChat.Data;
using DotnetChat.Data.Models;
using DotnetChat.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;
using DotnetChat.Extensions;

namespace DotnetChat
{
    public class ChatManager : IChatManager
    {
        private IChatRepository chatRepository;

        public ChatManager(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public Chat? GetChat(int id)
        {
            Chat? chat = chatRepository
                .Get(c => c.Id == id); ;

            return chat;
        }

        public IEnumerable<User> GetUsersInChat(Chat chat)
        {
            var users = chatRepository
                .Where(c => c.Id == chat.Id)
                .SelectMany(s => s.Members);

            return users;
        }

        public string GetChatName(Chat chat, User user)
        {
            if (!string.IsNullOrEmpty(chat.Name))
                return chat.Name;

            return chatRepository
                .Where(c => c.Id == chat.Id)
                .SelectMany(c => c.Members)
                .Where(f => f.Id != user.Id)
                .Select(s => s.Login)
                .First();
        }

        public bool HasUser(Chat chat, User user)
        {
            return chatRepository
                .Where(c => c.Id == chat.Id)
                .Where(c => c.Members.Contains(user))
                .Any();
        }

        public IEnumerable<int> GetMembersId(Chat chat)
        {
            return chatRepository
                .Where(c => c.Id == chat.Id)
                .SelectMany(s => s.Members.Select(s => s.Id))
                .ToList();
        }
    }
}
