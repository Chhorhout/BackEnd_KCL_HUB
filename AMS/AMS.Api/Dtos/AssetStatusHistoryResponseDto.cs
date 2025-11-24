using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class AssetStatusHistoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AssetName { get; set; }

    }
}