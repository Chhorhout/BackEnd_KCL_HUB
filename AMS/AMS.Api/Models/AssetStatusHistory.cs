namespace AMS.Api.Models
{
    public class AssetStatusHistory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
