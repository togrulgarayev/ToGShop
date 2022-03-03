using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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




            base.OnModelCreating(builder);
            
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }
}
