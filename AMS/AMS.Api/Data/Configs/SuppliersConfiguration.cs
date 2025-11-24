using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;

namespace AMS.Api.Data.Configs
{
    public class SuppliersConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Phone)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
