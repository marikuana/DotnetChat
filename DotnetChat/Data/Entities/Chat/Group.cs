namespace DotnetChat.Data.Models
{
    public class Group : Chat
    {
        public string Name { get; set; }

        public override string GetName()
        {
            return Name;
        }
    }
}
