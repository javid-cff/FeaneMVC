using FeaneMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FeaneMVC.Contexts
{
    public class FeaneDbContext : DbContext
    {
        public FeaneDbContext(DbContextOptions<FeaneDbContext> options) : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
