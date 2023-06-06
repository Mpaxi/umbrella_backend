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
            builder.Property(builder => builder.PaymentType).IsRequired();
            builder.Property(builder => builder.Number).IsRequired();
            builder.Property(builder => builder.CVV).IsRequired();
            builder.Property(builder => builder.CardName).IsRequired();
            builder.Property(builder => builder.ExpireAt).IsRequired();
            builder.Property(builder => builder.Quota).IsRequired();
            builder.Property(builder => builder.Rua).IsRequired();
            builder.Property(builder => builder.Numero).IsRequired();
            builder.Property(builder => builder.Complemento).IsRequired();
            builder.Property(builder => builder.Bairro).IsRequired();
            builder.Property(builder => builder.Cidade).IsRequired();
            builder.Property(builder => builder.UF).IsRequired();
            builder.Property(builder => builder.CEP).IsRequired();


            builder.HasMany(o => o.OrderProducts).WithOne(m => m.Order).HasForeignKey(f => f.OrderId);
        }
    }
}
