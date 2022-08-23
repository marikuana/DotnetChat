using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Models
{
    public class SendMessageModel
    {
        [Required]
        public int ChatId { get; set; }

        [Required, MaxLength(1024)]
        public string Text { get; set; }

        public int? ReplyMessage { get; set; }
    }
}
