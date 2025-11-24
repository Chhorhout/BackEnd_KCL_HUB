
namespace AMS.Api.Models
{
    public class Location
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Asset> Assets { get; set; }

    }
}