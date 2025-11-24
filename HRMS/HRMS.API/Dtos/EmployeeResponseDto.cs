using System.ComponentModel.DataAnnotations;
using HRMS.API.Models;
namespace HRMS.API.Dtos
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }

       public Guid DepartmentId { get; set; }

    }
}