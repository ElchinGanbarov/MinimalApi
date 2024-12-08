using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductMinimalApi.Models;

namespace ProductMinimalApi.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder. HasKey(x => x.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
        }
    }
}
