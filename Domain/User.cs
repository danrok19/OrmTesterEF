using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("username")]
        public required string Username { get; set; }

        [Column("password")]
        public required string Password { get; set; }

        [ForeignKey("AccountDeatails")]
        [Column("account_details_id")]
        public long? AccountDetailsId { get; set; }

        public virtual AccountDetails? AccountDetails { get; set; }

        //public virtual ICollection<Character>? Characters { get; set; }

        //public void add(Character character)
        //{
        //    if (Characters == null)
        //    {
        //        Characters = new List<Character>();
        //    }
        //    Characters.Add(character);
        //}

    }
}