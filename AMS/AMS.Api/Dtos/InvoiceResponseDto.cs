using System;

namespace AMS.Api.Dtos
{
    public class InvoiceResponseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public string TotalAmount { get; set; }
        public string Description { get; set; }
    }
    
}