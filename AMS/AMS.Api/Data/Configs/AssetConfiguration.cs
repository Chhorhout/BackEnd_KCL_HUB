using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;

namespace AMS.Api.Data.Configs
{
    public class AssetConfig : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.SerialNumber).IsRequired().HasMaxLength(50);
            builder.Property(a => a.HasWarranty).IsRequired();
            builder.Property(a => a.WarrantyStartDate).IsRequired(false);
            builder.Property(a => a.WarrantyEndDate).IsRequired(false);
            builder.HasOne(a => a.Location).WithMany(l => l.Assets).HasForeignKey(a => a.LocationId);
            builder.HasOne(a => a.Supplier).WithMany(s => s.Assets).HasForeignKey(a => a.SupplierId);
            builder.HasOne(a => a.AssetType).WithMany(at => at.Assets).HasForeignKey(a => a.AssetTypeId);
            builder.HasOne(a => a.Invoice).WithMany(i => i.Assets).HasForeignKey(a => a.InvoiceId);
            builder.HasMany(a => a.MaintenanceRecords).WithOne(mr => mr.Asset).HasForeignKey(a => a.AssetId);
            builder.HasMany(a => a.AssetOwnerShips).WithOne(aos => aos.Asset).HasForeignKey(a => a.AssetId);
            builder.HasMany(a => a.TemporaryUsedRequests).WithOne(tur => tur.Asset).HasForeignKey(a => a.AssetId);
            builder.HasMany(a => a.AssetStatusHistories).WithOne(ash => ash.Asset).HasForeignKey(a => a.AssetId);
        }
    }
}