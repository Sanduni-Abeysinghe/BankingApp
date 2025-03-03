using BankingSystemAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services.Interfaces
{
    public interface IBankBranchService
    {
        Task<IEnumerable<BankBranchDTO>> GetAllBankBranchesAsync();
        Task<BankBranchDTO> GetBankBranchByIdAsync(int id);
        Task<BankBranchDTO> CreateBankBranchAsync(BankBranchDTO bankBranchDTO);
        Task<bool> UpdateBankBranchAsync(int id, BankBranchDTO bankBranchDTO);
        Task<bool> DeleteBankBranchAsync(int id);
    }
}
