using Core.Entities;
using Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductImagesConfiguration());
            builder.ApplyConfiguration(new ContactAdminConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());




            base.OnModelCreating(builder);
            
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductOperation> ProductOperations { get; set; }
        public DbSet<ContactAdmin> ContactAdmin { get; set; }
        public DbSet<ProductComment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DiscountTimer> DiscountTimers { get; set; }

    }
}
