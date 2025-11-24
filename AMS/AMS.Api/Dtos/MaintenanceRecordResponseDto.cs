namespace AMS.Api.Dtos
{
    public class MaintenanceRecordResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MaintainerName { get; set; }
        public string AssetName { get; set; }
    }
}