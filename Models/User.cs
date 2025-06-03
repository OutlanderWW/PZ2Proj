using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestigationSupportSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string BadgeNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string Role { get; set; } // Admin, Officer, Coordinator
        public string Token { get; set; }
    }
}
