using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("equipment")]
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("type")]
        public required string Type { get; set; }

        [Column("statistics")]
        public int Statistics { get; set; }

        [Column("cost")]
        public int Cost { get; set; }

        public virtual ICollection<Character>? Characters { get; set; } = new List<Character>();

    }
}
