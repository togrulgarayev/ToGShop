using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
            builder.Property(b => b.CreateDT).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        }
    }
}
