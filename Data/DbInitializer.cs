using InvestigationSupportSystem.Models;
using InvestigationSupportSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestigationSupportSystem.Data
{
    
    public static class DbInitializer
    {
        
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();
            if (!context.Users.Any())
            {
                var hashing = new HashingService();
                var tokenService = new TokenService();

                var admin = new User
                {
                    BadgeNumber = "0000",
                    Name = "Administrator",
                    PasswordHash = hashing.HashPassword("admin123"),
                    Role = "Admin",
                    Token = tokenService.GenerateToken()
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
