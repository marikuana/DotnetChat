using DotnetChat.Data;
using DotnetChat.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetChat
{
    public class MessageManager : IMessageManager
    {
        private IMessageRepository messageRepository;

        public MessageManager(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public Message? GetMessage(int id)
        {
            return messageRepository
                .Where(m => m.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Message> GetMessages(Chat chat, int lastMessageId = int.MaxValue, int count = 1)
        {
            return messageRepository
                .Where(m => m.Chat.Id == chat.Id && m.Id < lastMessageId)
                .Where(m => m.Delete != Enums.MessageDelete.DeleteForAll)
                .OrderByDescending(m => m.CreatedDate)
                .Include(m => m.Author)
                .Take(count)
                .Reverse();
        }

        public bool IsAuthor(Message message, User author)
        {
            return messageRepository
                .Where(m => m.Id == message.Id)
                .Where(m => m.Author.Id == author.Id)
                .Any();
        }

        public Chat GetMessageChat(Message message)
        {
            return messageRepository
                .Where(m => m.Id == message.Id)
                .Select(m => m.Chat)
                .First();
        }

        public void CreateMessage(Message message)
        {
            messageRepository
                .Create(message);
        }

        public void UpdateMessage(Message message)
        {
            messageRepository
                .Update(message);
        }
    }
}
