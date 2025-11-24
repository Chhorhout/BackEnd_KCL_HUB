using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.API.Models;
namespace HRMS.API.Data.Configs
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(e => e.Email)
                .IsUnique();
            builder.HasIndex(e => e.PhoneNumber)
                .IsUnique();
            builder.Property(e => e.DateOfBirth)
                .IsRequired();
            builder.Property(e => e.HireDate)
                .IsRequired();
            builder.HasOne(e => e.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Always include Department when querying Employees
            builder.Navigation(e => e.Department).AutoInclude();
        }

    }
} 