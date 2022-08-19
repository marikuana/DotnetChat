using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Models
{
    public class LoginModel
    {
        [Required, MaxLength(64, ErrorMessage = "Max Lenght 64")]
        public string Login { get; set; }

        [Required, MaxLength(64, ErrorMessage = "Max Lenght 64")]
        public string Password { get; set; }
    }
}
