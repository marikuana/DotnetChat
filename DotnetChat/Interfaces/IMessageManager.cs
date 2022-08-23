using DotnetChat.Data.Models;

namespace DotnetChat
{
    public interface IMessageManager
    {
        void CreateMessage(Message message);
        IEnumerable<Message> GetMessages(Chat chat, int lastMessageId, int count);
        Message? GetMessage(int id);
        Chat GetMessageChat(Message message);
        bool IsAuthor(Message message, User author);
        void UpdateMessage(Message message);
    }
}