using Microsoft.EntityFrameworkCore;

namespace MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Titan> tabelaTitans => Set<Titan>();
    }
}


 