using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AMS.Api.Models;

namespace AMS.Api.Data.Configs
{
    public class MaintainerConfiguration : IEntityTypeConfiguration<Maintainer>
    {
        public void Configure(EntityTypeBuilder<Maintainer> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Phone).IsRequired().HasMaxLength(100);
        }
    }
}