using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;
namespace AMS.Api.Data.Configs
{
    public class TemporaryUsedRecordConfiguration : IEntityTypeConfiguration<TemporaryUsedRecord>
    {
        public void Configure(EntityTypeBuilder<TemporaryUsedRecord> builder)
        {
            builder.HasKey(tur => tur.Id);
            builder.Property(tur => tur.Name).IsRequired().HasMaxLength(100);
            builder.Property(tur => tur.Description).IsRequired(false).HasMaxLength(255);
            builder
                .HasMany(tur => tur.TemporaryUsedRequests)
                .WithOne(r => r.TemporaryUsedRecord)
                .HasForeignKey(r => r.TemporaryUsedRecordId);

            builder
                .HasOne(tur => tur.TemporaryUser)
                .WithMany(tu => tu.TemporaryUsedRecords)
                .HasForeignKey(tur => tur.TemporaryUserId);
            
        }
    }
}