using DotnetChat.Data;
using DotnetChat.Data.Models;

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
