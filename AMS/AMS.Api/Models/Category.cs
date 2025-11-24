
namespace AMS.Api.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<AssetType> AssetTypes { get; set; }
    }
}