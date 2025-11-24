using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public bool HasWarranty { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string LocationName { get; set; }
        public string SupplierName { get; set; }
        public string AssetTypeName { get; set; }
        public string InvoiceNumber { get; set; }
    }
}