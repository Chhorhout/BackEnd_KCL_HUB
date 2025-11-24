using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class MaintainerCreateDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }
        public Guid MaintainerTypeId { get; set; }
    }
}