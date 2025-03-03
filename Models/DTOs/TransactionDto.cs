using System;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemAPI.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int AccountId { get; set; }
    }

    public class CreateTransactionDTO
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int AccountId { get; set; }
    }
}
