using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public DateTime HireDate { get; set; }
        
        public Guid DepartmentId { get; set; }
    }
}