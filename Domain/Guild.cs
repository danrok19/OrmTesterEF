using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("guild")]
    public class Guild
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("level_progression")]
        public int LevelProgression { get; set; } = 0;

        public virtual ICollection<Character>? Characters { get; set; }

        public void add(Character character)
        {
            if (Characters == null)
            {
                Characters = new List<Character>();
            }
            Characters.Add(character);
        }
    }
}
