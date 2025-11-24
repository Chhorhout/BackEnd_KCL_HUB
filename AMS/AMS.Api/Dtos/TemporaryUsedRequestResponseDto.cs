using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class TemporaryUsedRequestResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TemporaryUsedRecordName { get; set; }
        public string AssetName { get; set; }
    }
}