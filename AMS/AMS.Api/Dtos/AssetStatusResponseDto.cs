using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetStatusResponseDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string AssetName { get; set; }
    }
}