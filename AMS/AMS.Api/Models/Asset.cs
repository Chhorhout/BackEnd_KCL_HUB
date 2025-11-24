namespace AMS.Api.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public bool HasWarranty { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public Guid LocationId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid AssetTypeId { get; set; }
        public Guid InvoiceId { get; set; }
        public Location Location { get; set; }
        public Supplier Supplier { get; set; }
        public AssetType AssetType { get; set; }
        public Invoice Invoice { get; set; }
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public ICollection<AssetOwnerShip> AssetOwnerShips { get; set; }
        public ICollection<TemporaryUsedRequest> TemporaryUsedRequests { get; set; }
        public ICollection<AssetStatusHistory> AssetStatusHistories { get; set; }
        public ICollection<AssetStatus> AssetStatuses { get; set; }
        public ICollection<TemporaryUsedRecord> TemporaryUsedRecords { get; set; }
    }
}