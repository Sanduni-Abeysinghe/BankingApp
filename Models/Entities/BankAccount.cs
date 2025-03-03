using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystemAPI.Models
{
    public class BankAccount
    //one-to-many relationship (one user can have many bank accounts)
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public int UserId { get; set; }  

        [ForeignKey("UserId")]
        public User User { get; set; }

        // One-to-many relationship with Transaction
    public ICollection<Transaction> Transactions { get; set; }
    }
}

