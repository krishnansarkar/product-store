using Microsoft.EntityFrameworkCore;
using ProductStore.Models;

namespace ProductStore.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Horror", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Comedy", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Action", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "It",
                    Description = "A horror novel by Stephen King.",
                    ISBN = "978-0450411434",
                    Author = "Stephen King",
                    Price = 9.99,
                    Price50 = 8.99,
                    Price100 = 7.99,
                    CategoryId = 1
                }
            );
        }
    }
}
