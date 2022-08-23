using DotnetChat.Data.Models;

namespace DotnetChat.Data
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(ChatContext chatContext) : base(chatContext)
        {
        }
    }
}
