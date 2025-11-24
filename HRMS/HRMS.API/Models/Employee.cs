using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public DateTime HireDate { get; set; } = DateTime.Now;
        // Navigation property
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}