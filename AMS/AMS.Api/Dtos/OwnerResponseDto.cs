using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class OwnerResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerTypeName { get; set; }
    }
}