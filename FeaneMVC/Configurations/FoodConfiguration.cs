using FeaneMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeaneMVC.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Category).WithMany(x => x.Foods).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
