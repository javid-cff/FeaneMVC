using Microsoft.AspNetCore.Identity;

namespace FeaneMVC.Models.AppUser
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
