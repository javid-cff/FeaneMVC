using System.ComponentModel.DataAnnotations;

namespace FeaneMVC.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can not be Empty! Please fill the name gap!"), 
            MaxLength(70, ErrorMessage = "Name length must be maximum 70 characters!"), 
                MinLength(3, ErrorMessage = "Name length must be minimum 3 characters!")]
        public string Name { get; set; } = string.Empty;
    }
}
