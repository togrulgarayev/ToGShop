using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductImagesConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(pi => pi.Image).IsRequired().HasMaxLength(255);
            builder.Property(pi => pi.IsMain).HasDefaultValue(false);
        }
    }
}
