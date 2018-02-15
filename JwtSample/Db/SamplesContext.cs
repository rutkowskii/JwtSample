using System.Data.Entity;

namespace Repetitions
{
    public class SamplesContext : DbContext
    {
        public SamplesContext() : base("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=piotrsamples;")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}