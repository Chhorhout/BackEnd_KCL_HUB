namespace AMS.Api.Models
{
    public class Maintainer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid MaintainerTypeId { get; set; }
        public MaintainerType MaintainerType { get; set; }
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
    }
}