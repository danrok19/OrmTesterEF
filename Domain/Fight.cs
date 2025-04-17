using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("fight")]
    public class Fight
    {
        [Column("character_id")]
        public long CharacterId { get; set; }
        public virtual Character? Character { get; set; }

        [Column("boss_id")]
        public long BossId { get; set; }
        public virtual Boss? Boss { get; set; }

        public bool IsCharacterWin { get; set; }

        [Column("time")]
        public DateTime Time { get; set; }
    }
}
