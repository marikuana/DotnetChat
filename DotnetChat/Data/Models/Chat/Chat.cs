namespace DotnetChat.Data.Models
{
    public abstract class Chat
    {
        public int Id { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
