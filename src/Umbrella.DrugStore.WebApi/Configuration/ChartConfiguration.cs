using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Umbrella.DrugStore.WebApi.Entities;

namespace Umbrella.DrugStore.WebApi.Configuration
{
    public class ChartConfiguration : IEntityTypeConfiguration<Chart>
    {
        public void Configure(EntityTypeBuilder<Chart> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Created);
            builder.Property(p => p.Updated);
            builder.Property(p => p.Active);

            builder.Property(p => p.ClientId);

            builder.HasMany(m => m.Products).WithOne(o => o.Chart).HasForeignKey(o => o.ChartId);
        }
    }
}
