namespace AMS.Api.Models
{
    public class MaintainerType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Maintainer> Maintainers { get; set; }
    }
}