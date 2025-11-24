using AMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMS.Api.Data.Configs
{
    public class MaintenacePartConfiguration : IEntityTypeConfiguration<MaintenacePart>
    {
        public void Configure(EntityTypeBuilder<MaintenacePart> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Description).HasMaxLength(255);
            
            builder.HasOne(e => e.MaintenanceRecord)
                .WithMany(mr => mr.MaintenaceParts)
                .HasForeignKey(e => e.MaintenaceRequestPartId);
        }
    }
}