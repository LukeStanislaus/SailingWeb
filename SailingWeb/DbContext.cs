using Microsoft.EntityFrameworkCore;
using SailingWeb.Data;

namespace RazorPagesContacts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Boats> Boatss { get; set; }
    }
}