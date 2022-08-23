using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Models
{
    public class DeleteMessageModel
    {
        [Required]
        public int MessageId { get; set; }
        public bool DeleteForMe { get; set; }
    }
}
