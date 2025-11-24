using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class CategoryCreateDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}