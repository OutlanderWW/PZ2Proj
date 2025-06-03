namespace InvestigationSupportSystem.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; } // Open, Closed, Archived

        public ICollection<Person> Persons { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<OfficerCase> OfficerCases { get; set; }
    //     public Case(string title, string desc, DateTime sd, string status)
    //     {
    //         Title = title;
    //         Description = desc;
    //         StartDate = sd;
    //         Status = status;
    // }
}
    }
