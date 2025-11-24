using System.ComponentModel.DataAnnotations;

namespace AMS.Api.Dtos
{
    public class InvoiceCreateDto
    {
        [MaxLength(50)]
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public string TotalAmount { get; set; }
        public string Description { get; set; }
    }
}