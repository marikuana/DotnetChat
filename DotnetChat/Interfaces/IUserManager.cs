using DotnetChat.Data.Models;

namespace DotnetChat
{
    public interface IUserManager
    {
        User? GetUser(int id);
        IEnumerable<Chat> GetUserChats(User user);
        bool HasAccess(User user, Chat chat);
    }
}