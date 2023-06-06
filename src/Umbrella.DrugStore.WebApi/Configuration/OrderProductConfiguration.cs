using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Configuration
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Created);
            builder.Property(p => p.Updated);
            builder.Property(p => p.Active);

            builder.HasOne(o => o.Order).WithMany(m => m.OrderProducts).HasForeignKey(f => f.OrderId);
            builder.HasOne(o => o.Product).WithMany(m => m.OrderProducts).HasForeignKey(f => f.ProductId);
        }
    }
}
