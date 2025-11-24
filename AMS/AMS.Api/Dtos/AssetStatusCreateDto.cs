using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetStatusCreateDto
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public Guid AssetId { get; set; }
    }
}