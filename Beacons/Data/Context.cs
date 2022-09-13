using Microsoft.EntityFrameworkCore;

namespace Beacons.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Beacon> Beacons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("BeaconsContainer");
            modelBuilder.Entity<Beacon>()
                .HasNoDiscriminator()
                .HasPartitionKey(x => x.Id);

            modelBuilder.Entity<Beacon>()
                .Property(x => x.TimeToLive)
                .ToJsonProperty("ttl");

            base.OnModelCreating(modelBuilder);
        }
    }
}
