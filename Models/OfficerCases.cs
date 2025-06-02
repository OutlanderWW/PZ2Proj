namespace InvestigationSupportSystem.Models
{
    public class OfficerCase
    {
        public int OfficerId { get; set; }
        public User Officer { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}