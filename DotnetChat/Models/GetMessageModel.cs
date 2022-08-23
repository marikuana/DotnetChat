using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Models
{
    public class GetMessageModel
    {
        [Required]
        public int ChatId { get; set; }
        [Range(1, 50)]
        public int Count { get; set; }
        public int LastMessageId { get; set; } = int.MaxValue;
    }
}
