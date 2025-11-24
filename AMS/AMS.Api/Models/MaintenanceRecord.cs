namespace AMS.Api.Models
{
    public class MaintenanceRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public Guid MaintainerId { get; set; }
        public Maintainer Maintainer { get; set; }
        public ICollection<MaintenacePart> MaintenaceParts { get; set; }
    }
}