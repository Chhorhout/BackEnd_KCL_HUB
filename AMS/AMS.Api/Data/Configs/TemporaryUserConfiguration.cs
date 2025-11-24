using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;
namespace AMS.Api.Data.Configs
{
    public class TemporaryUserConfiguration : IEntityTypeConfiguration<TemporaryUser>
    {
        public void Configure(EntityTypeBuilder<TemporaryUser> builder)
        {
            builder.HasKey(tu => tu.Id);
            builder.Property(tu => tu.Name).IsRequired().HasMaxLength(100);
            builder.Property(tu => tu.Description).IsRequired(false).HasMaxLength(255);
        }
    }
}