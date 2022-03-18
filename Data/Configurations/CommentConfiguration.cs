using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CommentConfiguration:IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.Property(c => c.CreateDT).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.Username).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Comment).IsRequired().HasColumnType("TEXT");
            builder.Property(c => c.IsDelete).HasDefaultValue(false);
        }
    }
}
