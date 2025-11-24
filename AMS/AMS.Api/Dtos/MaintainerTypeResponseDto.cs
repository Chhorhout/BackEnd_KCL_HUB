using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class MaintainerTypeResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}