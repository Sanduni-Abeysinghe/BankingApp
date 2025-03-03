using BankingSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingSystemAPI.Data
{
    public class BankingDbContext : IdentityDbContext<IdentityUser>
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> User { get; set; } 

    }
}
