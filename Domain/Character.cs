using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("characters")]
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("character_type")]
        public required string CharacterType { get; set; }

        [Column("currency")]
        public long Currency { get; set; }

        [Column("level_progression")]
        public int LevelProgression { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public long UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("Guild")]
        [Column("guild_id")]
        public long? GuildId { get; set; }
        public virtual Guild? Guild { get; set; }

        public virtual ICollection<Equipment>? Equipments { get; set; } = new List<Equipment>();

        public virtual ICollection<Fight>? Fights { get; set; }

        public void add(Equipment equip)
        {
            if (Equipments == null)
            {
                Equipments = new List<Equipment>();
            }
            Equipments.Add(equip);
        }


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
