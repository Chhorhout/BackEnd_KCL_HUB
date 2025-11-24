using AMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using AMS.Api.Data.Configs;
using System.Reflection;
namespace AMS.Api.Data

{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) :base(options)
        {
        }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Maintainer> Maintainers { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<AssetOwnerShip> AssetOwnerShips { get; set; }
        public DbSet<AssetStatus> AssetStatuses { get; set; }
        public DbSet<AssetStatusHistory> AssetStatusHistories { get; set; }
        public DbSet<MaintenacePart> MaintenanceParts { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerType> OwnerTypes { get; set; }
        public DbSet<TemporaryUsedRequest> TemporaryUsedRequests { get; set; }
        public DbSet<MaintainerType> MaintainerTypes { get; set; }
        public DbSet<TemporaryUser> TemporaryUsers { get; set; }
        public DbSet<TemporaryUsedRecord> TemporaryUsedRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from the assembly in folder Configs
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}