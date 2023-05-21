using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Created);
            builder.Property(p => p.Updated);
            builder.Property(p => p.Active);

            builder.Property(p => p.Name);
            builder.Property(p => p.Description);
            builder.Property(p => p.Price);
            builder.Property(p => p.Unit);
            builder.Property(p => p.Rating);

            builder.HasMany(o => o.Images).WithOne(m => m.Product).HasForeignKey(f => f.ProductId);
        }
    }
}
