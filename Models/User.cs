namespace InvestigationSupportSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string BadgeNumber { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin, Officer, Coordinator
        public string Token { get; set; }
    }
}
