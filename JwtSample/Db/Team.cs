using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repetitions
{
    [Table("Team")]
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}