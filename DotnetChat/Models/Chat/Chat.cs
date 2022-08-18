namespace DotnetChat.Models
{
    public abstract class Chat
    {
        public Guid Id { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
