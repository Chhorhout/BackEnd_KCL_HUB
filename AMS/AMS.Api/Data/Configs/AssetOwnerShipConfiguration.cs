using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;

namespace AMS.Api.Data.Configs
{
    public class AssetOwnerShipConfiguration : IEntityTypeConfiguration<AssetOwnerShip>
    {
        public void Configure(EntityTypeBuilder<AssetOwnerShip> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(a => a.Asset).WithMany(a => a.AssetOwnerShips).HasForeignKey(a => a.AssetId);
        }
    }
}