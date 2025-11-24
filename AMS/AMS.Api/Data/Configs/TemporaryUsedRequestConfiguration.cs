using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;
namespace AMS.Api.Data.Configs
{
    public class TemporaryUsedRequestConfiguration : IEntityTypeConfiguration<TemporaryUsedRequest>
    {
        public void Configure(EntityTypeBuilder<TemporaryUsedRequest> builder)
        {
            builder.HasKey(tur => tur.Id);
            builder.Property(tur => tur.Name).IsRequired().HasMaxLength(100);
            builder.Property(tur => tur.Description).IsRequired(false).HasMaxLength(255);
            builder
                .HasOne(r => r.TemporaryUsedRecord)
                .WithMany(rec => rec.TemporaryUsedRequests)
                .HasForeignKey(r => r.TemporaryUsedRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Asset)
                .WithMany(a => a.TemporaryUsedRequests)
                .HasForeignKey(r => r.AssetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}