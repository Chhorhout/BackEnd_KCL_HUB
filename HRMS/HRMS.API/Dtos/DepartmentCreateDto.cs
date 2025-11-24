using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Dtos
{
    public class DepartmentCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}