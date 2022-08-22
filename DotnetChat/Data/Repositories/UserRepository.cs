using DotnetChat.Data.Models;

namespace DotnetChat.Data
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ChatContext chatContext) : base(chatContext)
        {
        }
    }
}
