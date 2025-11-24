namespace AMS.Api.Models
{
    public class TemporaryUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    

    public ICollection<TemporaryUsedRecord> TemporaryUsedRecords { get; set; }
    }
}