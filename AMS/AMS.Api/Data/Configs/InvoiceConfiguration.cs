using Microsoft.EntityFrameworkCore;
using AMS.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Api.Data.Configs
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Number).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Date).IsRequired(false);
            builder.Property(i => i.TotalAmount).IsRequired();
            builder.Property(i => i.Description).IsRequired(false);
            builder.HasMany(i => i.Assets).WithOne(a => a.Invoice).HasForeignKey(a => a.InvoiceId);
        }
    }
}