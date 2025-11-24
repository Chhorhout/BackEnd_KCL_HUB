using System.ComponentModel.DataAnnotations;
namespace AMS.Api.Dtos
{
    public class AssetStatusHistoryCreateDto
    {
        public string Name { get; set; }
        public Guid AssetId { get; set; }
    }
}