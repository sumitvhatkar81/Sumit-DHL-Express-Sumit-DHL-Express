
using Microsoft.EntityFrameworkCore;
using DHL_Document_App.Models;

namespace DHL_Document_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
