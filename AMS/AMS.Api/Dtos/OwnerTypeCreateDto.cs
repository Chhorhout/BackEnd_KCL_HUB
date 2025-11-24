using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class OwnerTypeCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}