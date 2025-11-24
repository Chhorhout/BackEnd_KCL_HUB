using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class TemporaryUsedRequestCreateDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public Guid TemporaryUsedRecordId { get; set; }
        public Guid AssetId { get; set; }
    }
}