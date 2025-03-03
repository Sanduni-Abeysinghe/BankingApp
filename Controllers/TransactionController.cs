using BankingSystemAPI.DTOs;
using BankingSystemAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            return Ok(await _transactionService.GetAllTransactionsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionDTO transactionDTO)
        {
            if (await _transactionService.CreateTransactionAsync(transactionDTO))
                return CreatedAtAction(nameof(GetTransaction), new { id = transactionDTO.AccountId }, transactionDTO);

            return BadRequest();
        }
    }
}
