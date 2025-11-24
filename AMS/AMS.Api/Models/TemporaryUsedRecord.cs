namespace AMS.Api.Models
{
    public class TemporaryUsedRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        
        public Guid TemporaryUserId { get; set; }
        public TemporaryUser TemporaryUser { get; set; }
        public ICollection<TemporaryUsedRequest> TemporaryUsedRequests { get; set; }
    
    }
}