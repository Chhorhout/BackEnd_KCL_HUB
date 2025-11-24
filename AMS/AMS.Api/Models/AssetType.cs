namespace AMS.Api.Models
{
    public class AssetType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Asset> Assets { get; set; } 
    }
}