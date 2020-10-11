using Entities.ModelConfiguration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<PositionTransaction> PositionTransactions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RoleMaster> RoleMaster { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasIndex(i => i.Email)
                .IsClustered(false);

            modelBuilder.Entity<Vehicle>()
                .HasIndex(i => i.LicensePlateNumber)
                .IsClustered(false);

            modelBuilder.Entity<PositionTransaction>()
                .HasIndex(i => new { i.DiviseId, i.TransactionDate })
                .IsClustered(false);

            modelBuilder.ApplyConfiguration(new RoleMasterConfiguration());
        }

    }
}
