namespace InvestigationSupportSystem.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
