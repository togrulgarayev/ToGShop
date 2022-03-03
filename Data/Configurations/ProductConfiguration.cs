﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Price).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Description).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Count).IsRequired();
            builder.Property(p => p.Information).IsRequired().HasColumnType("TEXT");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.CreateDT).HasDefaultValueSql("GETUTCDATE()");
            
        }   
    }
}
