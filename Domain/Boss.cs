using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("boss")]
    public class Boss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("difficulty_level")]
        public int DifficultyLevel { get; set; }

        public virtual ICollection<Fight>? Fights { get; set; }

        public void add(Fight fight)
        {
            if (Fights == null)
            {
                Fights = new List<Fight>();
            }
            Fights.Add(fight);
        }

    }
}
