using CorgiVR.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace CorgiVR.Repository
{
    public class EfContext : DbContext
    {
        // private readonly string _connectionString;
        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasKey(x => x.Id);
        }
    }
}