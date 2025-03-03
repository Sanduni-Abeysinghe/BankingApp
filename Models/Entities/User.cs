using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace BankingSystemAPI.Models
{
    public class User 
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

[JsonIgnore]
        //one-to-many relationship. one user can have many bank accounts

        public ICollection<BankAccount> BankAccounts { get; set; }

    }
}
