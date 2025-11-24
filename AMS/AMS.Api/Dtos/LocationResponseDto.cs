using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class LocationResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}