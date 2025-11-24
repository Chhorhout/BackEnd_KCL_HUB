using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace HRMS.API.Dtos
{
    public class DepartmentResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}