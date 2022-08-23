using DotnetChat.Data;
using DotnetChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DotnetChat
{
    public class UserManager : IUserManager
    {
        private IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User? GetUser(int id)
        {
            User? user = userRepository
                .Get(u => u.Id == id);

            return user;
        }

        public IEnumerable<Chat> GetUserChats(User user)
        {
            var chats = userRepository
                .Where(u => u.Id == user.Id)
                .SelectMany(s => s.Chats)
                .ToList();

            return chats;
        }

        public bool HasAccess(User user, Chat chat)
        {
            return userRepository
                .Where(u => u.Id == user.Id)
                .Where(u => u.Chats.Contains(chat))
                .Any();
        }
    }
}
