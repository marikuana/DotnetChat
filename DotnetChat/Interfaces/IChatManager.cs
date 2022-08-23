using DotnetChat.Data.Models;

namespace DotnetChat
{
    public interface IChatManager
    {
        Chat? GetChat(int id);
        string GetChatName(Chat chat, User user);
        IEnumerable<int> GetMembersId(Chat chat);
        IEnumerable<User> GetUsersInChat(Chat chat);
        bool HasUser(Chat chat, User user);
    }
}