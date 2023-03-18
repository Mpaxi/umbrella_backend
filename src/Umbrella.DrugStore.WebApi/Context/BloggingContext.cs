namespace Umbrella.DrugStore.WebApi.Context
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class BloggingContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public BloggingContext([NotNull] DbContextOptions options) : base(options)
        {
        }
    }

}
