using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;
namespace AMS.Api.Data.Configs
{
    public class AssetStatusConfig : IEntityTypeConfiguration<AssetStatus>
    {
        public void Configure(EntityTypeBuilder<AssetStatus> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Status).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(255);
            builder.HasOne(a => a.Asset).WithMany(a => a.AssetStatuses).HasForeignKey(a => a.AssetId);
        }
    }
}
