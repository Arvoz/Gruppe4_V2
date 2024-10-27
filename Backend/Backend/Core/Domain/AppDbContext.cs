using Microsoft.EntityFrameworkCore;

namespace Backend.Core.Domain
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }

    }
}
