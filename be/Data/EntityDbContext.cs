using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
        }

        public virtual DbSet<PlayRound> PlayRounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayRound>();
        }
    }
}
