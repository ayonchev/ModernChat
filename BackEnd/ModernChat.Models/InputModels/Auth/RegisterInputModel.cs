using System.ComponentModel.DataAnnotations;

namespace ModernChat.Models.InputModels.Auth
{
    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }

        [Required]
        [MinLength(1)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
