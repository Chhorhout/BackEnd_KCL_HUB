using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class SupplierCreateDto
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
}