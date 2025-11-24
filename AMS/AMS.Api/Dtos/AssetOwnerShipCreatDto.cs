using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetOwnerShipCreateDto
    {
        public string Name { get; set; }
        public Guid AssetId { get; set; }
        public Guid OwnerId { get; set; }
    }
}