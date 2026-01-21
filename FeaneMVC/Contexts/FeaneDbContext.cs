using System.Reflection;
using System.Reflection.Emit;
using FeaneMVC.Models;
using FeaneMVC.Models.AppUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeaneMVC.Contexts
{
    public class FeaneDbContext : IdentityDbContext<AppUser>
    {
        public FeaneDbContext(DbContextOptions<FeaneDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //    base.OnModelCreating(builder);
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    base.OnModelCreating(builder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
