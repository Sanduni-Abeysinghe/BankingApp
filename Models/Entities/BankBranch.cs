using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemAPI.Models
{
    public class BankBranch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<BankAccount> Accounts { get; set; }
    }
}

// Defines a One-to-Many relationship with Account.
// Id is the primary key.
// Accounts → One branch can have multiple accounts.