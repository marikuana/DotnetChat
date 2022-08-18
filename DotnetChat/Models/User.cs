namespace DotnetChat.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
    }
}
