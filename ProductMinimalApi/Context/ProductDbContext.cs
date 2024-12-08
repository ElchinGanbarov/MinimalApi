using Microsoft.EntityFrameworkCore;
using ProductMinimalApi.Configurations;
using ProductMinimalApi.Models;

namespace ProductMinimalApi.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
