using System.ComponentModel.DataAnnotations;

namespace InvestigationSupportSystem.Models
{
    public class Case
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Status { get; set; } = "Open";
        public DateTime StartDate { get; set; }

        public ICollection<Person> Persons { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
