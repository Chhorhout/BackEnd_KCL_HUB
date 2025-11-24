namespace AMS.Api.Models
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid OwnerTypeId { get; set; }
        public OwnerType OwnerType { get; set; }
        public ICollection<AssetOwnerShip> AssetOwnerShips { get; set; }
    }
}