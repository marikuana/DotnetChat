using DotnetChat.Data.Models;

namespace DotnetChat.Data
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(ChatContext chatContext) : base(chatContext)
        {
        }
    }
}
