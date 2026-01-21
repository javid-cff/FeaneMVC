namespace FeaneMVC.Models
{
    public class Food : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
