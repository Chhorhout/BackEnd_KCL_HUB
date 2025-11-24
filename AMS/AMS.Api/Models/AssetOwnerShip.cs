namespace AMS.Api.Models
{
    public class AssetOwnerShip
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public Owner Owner { get; set; }
        public Guid OwnerId { get; set; }
    }
}