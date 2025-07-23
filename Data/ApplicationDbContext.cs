using Microsoft.EntityFrameworkCore;
using JohnDuck.Models;

namespace JohnDuck.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
