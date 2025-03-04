using BankingSystemAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
