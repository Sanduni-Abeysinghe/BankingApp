using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemAPI.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // one-to-many relationship. One Role can have many Users
        public ICollection<User> Users { get; set; }
    }
}
