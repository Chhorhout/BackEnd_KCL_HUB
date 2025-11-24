
namespace AMS.Api.Dtos
{
    public class MaintenacePartResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid MaintenanceRecordId { get; set; }
    }
}