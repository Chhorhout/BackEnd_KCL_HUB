namespace AMS.Api.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }

        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public string TotalAmount { get; set; }
        public string Description { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
