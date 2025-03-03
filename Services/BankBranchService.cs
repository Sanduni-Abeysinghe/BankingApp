using AutoMapper;
using BankingSystemAPI.DTOs;
using BankingSystemAPI.Models;
using BankingSystemAPI.Repositories;
using BankingSystemAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services
{
    public class BankBranchService : IBankBranchService
    {
        private readonly BankBranchRepository _bankBranchRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BankBranchService> _logger;

        public BankBranchService(BankBranchRepository bankBranchRepository, IMapper mapper, ILogger<BankBranchService> logger)
        {
            _bankBranchRepository = bankBranchRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BankBranchDTO>> GetAllBankBranchesAsync()
        {
            try
            {
                var branches = await _bankBranchRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<BankBranchDTO>>(branches);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving bank branches: {ex.Message}");
                throw;
            }
        }

        public async Task<BankBranchDTO> GetBankBranchByIdAsync(int id)
        {
            try
            {
                var branch = await _bankBranchRepository.GetByIdAsync(id);
                if (branch == null) return null;

                return _mapper.Map<BankBranchDTO>(branch);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving bank branch with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<BankBranchDTO> CreateBankBranchAsync(BankBranchDTO bankBranchDTO)
        {
            try
            {
                var branch = _mapper.Map<BankBranch>(bankBranchDTO);
                await _bankBranchRepository.AddAsync(branch);
                return _mapper.Map<BankBranchDTO>(branch);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating bank branch: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateBankBranchAsync(int id, BankBranchDTO bankBranchDTO)
        {
            try
            {
                var branch = await _bankBranchRepository.GetByIdAsync(id);
                if (branch == null) return false;

                _mapper.Map(bankBranchDTO, branch);
                await _bankBranchRepository.UpdateAsync(branch);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating bank branch with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteBankBranchAsync(int id)
        {
            try
            {
                var branch = await _bankBranchRepository.GetByIdAsync(id);
                if (branch == null) return false;

                await _bankBranchRepository.DeleteAsync(branch);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting bank branch with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
