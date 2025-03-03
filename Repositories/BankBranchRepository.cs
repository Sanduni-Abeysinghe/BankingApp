using BankingSystemAPI.Data;
using BankingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystemAPI.Repositories
{
    public class BankBranchRepository 
    {
        private readonly BankingDbContext _context;

        public BankBranchRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BankBranch>> GetAllAsync()
        {
            return await _context.BankBranches.ToListAsync();
        }

        public async Task<BankBranch> GetByIdAsync(int id)
        {
            return await _context.BankBranches.FindAsync(id);
        }

        public async Task AddAsync(BankBranch bankBranch)
        {
            _context.BankBranches.Add(bankBranch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BankBranch bankBranch)
        {
            _context.Entry(bankBranch).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BankBranch bankBranch)
        {
            _context.BankBranches.Remove(bankBranch);
            await _context.SaveChangesAsync();
        }
    }
}
