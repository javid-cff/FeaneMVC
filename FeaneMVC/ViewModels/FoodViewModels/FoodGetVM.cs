using FeaneMVC.Models;

namespace FeaneMVC.ViewModels.FoodViewModels
{
    public class FoodGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;
    }
}
