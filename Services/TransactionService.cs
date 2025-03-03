using BankingSystemAPI.DTOs;
using BankingSystemAPI.Models;
using BankingSystemAPI.Repositories;
using BankingSystemAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(TransactionRepository transactionRepository, ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            _logger.LogInformation("Fetching all transactions.");
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions.Select(t => new TransactionDTO
            {
                Id = t.Id,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                AccountId = t.AccountId
            });
        }

        public async Task<TransactionDTO> GetTransactionByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching transaction with ID: {id}");
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                _logger.LogWarning($"Transaction with ID {id} not found.");
                return null;
            }

            return new TransactionDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId
            };
        }

        public async Task<bool> CreateTransactionAsync(CreateTransactionDTO transactionDTO)
        {
            try
            {
                _logger.LogInformation("Creating a new transaction.");
                var transaction = new Transaction
                {
                    Amount = transactionDTO.Amount,
                    TransactionDate = transactionDTO.TransactionDate,
                    AccountId = transactionDTO.AccountId
                };

                await _transactionRepository.AddAsync(transaction);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating transaction: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateTransactionAsync(TransactionDTO transactionDTO)
        {
            try
            {
                _logger.LogInformation($"Updating transaction ID: {transactionDTO.Id}");

                var existingTransaction = await _transactionRepository.GetByIdAsync(transactionDTO.Id);
                if (existingTransaction == null)
                {
                    _logger.LogWarning($"Transaction with ID {transactionDTO.Id} not found.");
                    return false;
                }

                existingTransaction.Amount = transactionDTO.Amount;
                existingTransaction.TransactionDate = transactionDTO.TransactionDate;
                existingTransaction.AccountId = transactionDTO.AccountId;

                await _transactionRepository.UpdateAsync(existingTransaction);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating transaction: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting transaction with ID: {id}");

                var transaction = await _transactionRepository.GetByIdAsync(id);
                if (transaction == null)
                {
                    _logger.LogWarning($"Transaction with ID {id} not found.");
                    return false;
                }

                await _transactionRepository.DeleteAsync(transaction);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting transaction: {ex.Message}");
                return false;
            }
        }
    }
}
