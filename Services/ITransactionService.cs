using BankingSystemAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO> GetTransactionByIdAsync(int id);
        Task<bool> CreateTransactionAsync(CreateTransactionDTO transactionDTO);
        Task<bool> UpdateTransactionAsync(TransactionDTO transactionDTO);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
