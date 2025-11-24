namespace AMS.Api.Models
{
    public class AssetStatus
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}