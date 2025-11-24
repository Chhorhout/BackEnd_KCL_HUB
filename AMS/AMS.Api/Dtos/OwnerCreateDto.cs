using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class OwnerCreateDto
    {
        public string Name { get; set; }
        public Guid OwnerTypeId { get; set; }
    }
}