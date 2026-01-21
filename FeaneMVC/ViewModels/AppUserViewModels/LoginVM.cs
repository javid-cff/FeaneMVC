using System.ComponentModel.DataAnnotations;

namespace FeaneMVC.ViewModels.AppUserViewModels
{
    public class LoginVM
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), MaxLength(255), MinLength(8)]
        public string Password { get; set; } = string.Empty;
        public bool IsRemember { get; set; }
    }
}
