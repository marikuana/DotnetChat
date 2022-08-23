using DotnetChat.Data.Models;

namespace DotnetChat.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChatContext chatContext) : base(chatContext)
        {
        }
    }
}
