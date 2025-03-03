using BankingSystemAPI.DTOs;
using BankingSystemAPI.Repositories;
using BankingSystemAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services
{
    public class RolesService : IRolesService
    {
        private readonly RolesRepository _rolesRepository;
        private readonly ILogger<RolesService> _logger;

        public RolesService(RolesRepository rolesRepository, ILogger<RolesService> logger)
        {
            _rolesRepository = rolesRepository;
            _logger = logger;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            _logger.LogInformation("Fetching all roles via service.");
            return _rolesRepository.GetAllRoles();
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            _logger.LogInformation($"Fetching role with ID: {roleId} via service.");
            return await _rolesRepository.GetRoleByIdAsync(roleId);
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleDTO createRoleDTO)
        {
            _logger.LogInformation($"Creating new role: {createRoleDTO.RoleName} via service.");
            return await _rolesRepository.CreateRoleAsync(createRoleDTO);
        }

        public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO)
        {
            _logger.LogInformation($"Updating role with ID: {updateRoleDTO.RoleId} via service.");
            return await _rolesRepository.UpdateRoleAsync(updateRoleDTO);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            _logger.LogInformation($"Deleting role with ID: {roleId} via service.");
            return await _rolesRepository.DeleteRoleAsync(roleId);
        }

        public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDTO assignRoleDTO)
        {
            _logger.LogInformation($"Assigning role {assignRoleDTO.RoleName} to user {assignRoleDTO.UserId} via service.");
            return await _rolesRepository.AssignRoleToUserAsync(assignRoleDTO);
        }
    }
}
