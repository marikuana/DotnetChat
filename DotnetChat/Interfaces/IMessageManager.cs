using DotnetChat.Data.Models;

namespace DotnetChat
{
    public interface IMessageManager
    {
        void CreateMessage(Message message);
        Message? GetMessage(int id);
        Chat GetMessageChat(Message message);
        bool IsAuthor(Message message, User author);
        void UpdateMessage(Message message);
    }
}