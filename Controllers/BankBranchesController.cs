using System.Collections.Generic;
using System.Threading.Tasks;
using BankingSystemAPI.DTOs;
using BankingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankBranchesController : ControllerBase
    {
        private readonly BankBranchService _bankBranchService;
        private readonly ILogger<BankBranchesController> _logger;

        public BankBranchesController(BankBranchService bankBranchService, ILogger<BankBranchesController> logger)
        {
            _bankBranchService = bankBranchService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankBranchDTO>>> GetBankBranches()
        {
            var branches = await _bankBranchService.GetAllBankBranchesAsync();
            return Ok(branches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankBranchDTO>> GetBankBranch(int id)
        {
            var bankBranch = await _bankBranchService.GetBankBranchByIdAsync(id);

            if (bankBranch == null)
            {
                _logger.LogWarning($"Bank branch with ID {id} not found.");
                return NotFound("Bank branch not found.");
            }

            return Ok(bankBranch);
        }

        [HttpPost]
        public async Task<ActionResult<BankBranchDTO>> PostBankBranch(BankBranchDTO bankBranchDTO)
        {
            var createdBranch = await _bankBranchService.CreateBankBranchAsync(bankBranchDTO);
            return CreatedAtAction(nameof(GetBankBranch), new { id = createdBranch.Id }, createdBranch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankBranch(int id, BankBranchDTO bankBranchDTO)
        {
            var success = await _bankBranchService.UpdateBankBranchAsync(id, bankBranchDTO);
            if (!success)
            {
                _logger.LogWarning($"Failed to update bank branch with ID {id}. Not found.");
                return NotFound("Bank branch not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankBranch(int id)
        {
            var success = await _bankBranchService.DeleteBankBranchAsync(id);
            if (!success)
            {
                _logger.LogWarning($"Failed to delete bank branch with ID {id}. Not found.");
                return NotFound("Bank branch not found.");
            }

            return NoContent();
        }
    }
}
