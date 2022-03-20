using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ContactAdminConfiguration : IEntityTypeConfiguration<ContactAdmin>
    {
        public void Configure(EntityTypeBuilder<ContactAdmin> builder)
        {
            builder.Property(p => p.Message).IsRequired().HasColumnType("TEXT");
            builder.Property(c => c.CreateDT).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
