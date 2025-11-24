using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetTypeCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}