using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class Department
    {
        public Guid Id { get; set; }
                public string Name { get; set; }
        // Navigation property
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}