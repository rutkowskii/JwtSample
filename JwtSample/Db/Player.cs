using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}