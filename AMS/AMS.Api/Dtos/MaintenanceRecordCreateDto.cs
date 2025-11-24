using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class MaintenanceRecordCreateDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public Guid AssetId { get; set; }
        public Guid MaintainerId { get; set; }
    }
}