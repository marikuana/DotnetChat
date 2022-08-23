using DotnetChat.Data.Models;

namespace DotnetChat.Data
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ChatContext chatContext) : base(chatContext)
        {
        }
    }
}
