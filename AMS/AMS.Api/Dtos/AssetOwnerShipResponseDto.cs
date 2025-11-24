using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetOwnerShipResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AssetName { get; set; }
        public string OwnerName { get; set; }
    }
}