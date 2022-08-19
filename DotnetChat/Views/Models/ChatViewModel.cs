namespace DotnetChat.Models
{
    public class ChatViewModel
    {
        public string Name { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
