namespace DotnetChat.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
