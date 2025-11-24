namespace AMS.Api.Models
{
    public class OwnerType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Owner> Owners { get; set; }
    }
}