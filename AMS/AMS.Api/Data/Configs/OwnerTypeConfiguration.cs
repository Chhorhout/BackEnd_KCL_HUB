using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;
namespace AMS.Api.Data.Configs
{
    public class OwnerTypeConfiguration : IEntityTypeConfiguration<OwnerType>
    {
        public void Configure(EntityTypeBuilder<OwnerType> builder)
        {
            builder.HasKey(ot => ot.Id);
            builder.Property(ot => ot.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(ot => ot.Owners).WithOne(o => o.OwnerType).HasForeignKey(o => o.OwnerTypeId);
            builder.Property(ot => ot.Description).IsRequired(false).HasMaxLength(255);
        }
    }
}