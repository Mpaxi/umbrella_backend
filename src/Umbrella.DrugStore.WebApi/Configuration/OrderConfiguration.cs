using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Created);
            builder.Property(p => p.Updated);
            builder.Property(p => p.Active);
            builder.Property(p => p.OrdeId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(builder => builder.UserId).IsRequired();
            builder.Property(builder => builder.Status).IsRequired();

            builder.HasMany(o => o.OrderProducts).WithOne(m => m.Order).HasForeignKey(f => f.OrderId);
        }
    }
}
