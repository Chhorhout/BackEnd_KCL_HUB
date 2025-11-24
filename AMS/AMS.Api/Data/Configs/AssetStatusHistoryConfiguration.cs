using AMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Api.Data.Configs
{
    public class AssetStatusHistoryConfiguration : IEntityTypeConfiguration<AssetStatusHistory>
    {
        public void Configure(EntityTypeBuilder<AssetStatusHistory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.HasOne(e => e.Asset).WithMany(e => e.AssetStatusHistories).HasForeignKey(e => e.AssetId);
        }
    }
}