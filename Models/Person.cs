namespace InvestigationSupportSystem.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string RoleInCase { get; set; } // Witness, Suspect

        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
