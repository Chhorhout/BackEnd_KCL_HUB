namespace AMS.Api.Models
{
    public class MaintenacePart
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid MaintenaceRequestPartId { get; set; }
        public MaintenanceRecord MaintenanceRecord { get; set; }
    }
}