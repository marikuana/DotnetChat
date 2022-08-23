using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Models
{
    public class EditMessageModel
    {
        [Required]
        public int MessageId { get; set; }
        [Required]
        public string NewText { get; set; }
    }
}
