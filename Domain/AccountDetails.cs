using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("account_details")]
    public class AccountDetails
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("is_premium")]
        public Boolean IsPremium { get; set; }

        [Column("signup_date")]
        public DateTime SignupDate { get; set; }

        public virtual User? User { get; set; }

    }
}