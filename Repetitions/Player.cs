using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Repetitions
{
    [Table("Player")]
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Team Team { get; set; }
    }

    [Table("Team")]
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
    }

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