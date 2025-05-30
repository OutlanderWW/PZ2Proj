using Microsoft.EntityFrameworkCore;
using InvestigationSupportSystem.Models;
using Microsoft.Data.Sqlite;

namespace InvestigationSupportSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Document> Documents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
