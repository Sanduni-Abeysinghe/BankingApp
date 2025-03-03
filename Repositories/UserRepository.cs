using BankingSystemAPI.Data;
using BankingSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystemAPI.Repositories
{
    public class UserRepository
    {
        private readonly BankingDbContext _context;

        public UserRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.User.ToListAsync();

        public async Task<User> GetByIdAsync(int id) => await _context.User.FindAsync(id);

        public async Task AddAsync(User student)
        {
            _context.User.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.User.FindAsync(id);
            if (student != null)
            {
                _context.User.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
