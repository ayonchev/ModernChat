using System.ComponentModel.DataAnnotations;

namespace ModernChat.Models.InputModels.Auth
{
    public class LoginIM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }
    }
}
