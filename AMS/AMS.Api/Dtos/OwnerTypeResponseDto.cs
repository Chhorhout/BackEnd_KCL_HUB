using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class OwnerTypeResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}