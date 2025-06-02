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
                var sample = new User
                {
                    BadgeNumber = "1111", Name="Sample", PasswordHash = hashing.HashPassword("sample123sa"), Role="Officer", Token=tokenService.GenerateToken()

                };
                var samplecase = new Case
                {
                    Title ="Sample Case", Description="This is a sample case for testing", StartDate=new DateTime(2000, 05,05), Status="Open"
                };
                var table = new OfficerCase
                {
                    OfficerId= 2, Officer=sample, CaseId=1, Case=samplecase
                };

                context.Users.Add(admin);
                context.Users.Add(sample);
                context.Cases.Add(samplecase);
                context.OfficerCases.Add(table);
                context.SaveChanges();
            }
        }
    }
}
