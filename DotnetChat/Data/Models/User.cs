namespace DotnetChat.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
    }
}
