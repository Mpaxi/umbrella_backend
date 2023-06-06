namespace Umbrella.DrugStore.WebApi.Context
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;
    using Umbrella.DrugStore.WebApi.Auth;
    using Umbrella.DrugStore.WebApi.Configuration;
    using Umbrella.DrugStore.WebApi.Entities;

    public class BloggingContext : IdentityDbContext<UserEntity>
    {
        public BloggingContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(builder);

            
        }



    }

}
