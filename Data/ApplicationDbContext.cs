using Microsoft.EntityFrameworkCore;
using ProductStore.Models;

namespace ProductStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
