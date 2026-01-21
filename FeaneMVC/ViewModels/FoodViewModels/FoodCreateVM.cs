using System.ComponentModel.DataAnnotations;

namespace FeaneMVC.ViewModels.FoodViewModels
{
    public class FoodCreateVM
    {
        [Required(ErrorMessage = "Name can not be Empty! Please fill the name gap!"),
            MaxLength(70, ErrorMessage = "Name length must be maximum 70 characters!"),
                MinLength(3, ErrorMessage = "Name length must be minimum 3 characters!")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500, ErrorMessage = "Description length must be maximum 500 characters!"),
                MinLength(20, ErrorMessage = "Description length must be minimum 20 characters!")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price can not be Empty! Please enter the food price!"), Range(0, double.MaxValue, ErrorMessage = "Price can not be negative!")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Image can not be Empty! Please put your image!")]
        public IFormFile Image { get; set; } = null!;
        [Required(ErrorMessage = "Category can not be Empty! Please select the category!")]
        public int CategoryId { get; set; }
    }
}
