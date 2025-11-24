using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class TemporaryUserCreateDto
    {
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}