using BankingSystemAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystemAPI.Repositories
{
    public class RolesRepository 
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RolesRepository> _logger;

        public RolesRepository(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ILogger<RolesRepository> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            _logger.LogInformation("Fetching all roles from repository.");
            return _roleManager.Roles.ToList();
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            _logger.LogInformation($"Fetching role with ID: {roleId} from repository.");
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityResult> CreateRoleAsync(CreateRoleDTO createRoleDTO)
        {
            _logger.LogInformation($"Creating new role: {createRoleDTO.RoleName} in repository.");
            var role = new IdentityRole(createRoleDTO.RoleName);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO)
        {
            _logger.LogInformation($"Updating role with ID: {updateRoleDTO.RoleId} in repository.");

            var role = await _roleManager.FindByIdAsync(updateRoleDTO.RoleId);
            if (role == null)
            {
                _logger.LogWarning($"Role with ID {updateRoleDTO.RoleId} not found.");
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }

            role.Name = updateRoleDTO.NewRoleName;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            _logger.LogInformation($"Deleting role with ID: {roleId} from repository.");

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                _logger.LogWarning($"Role with ID {roleId} not found.");
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }

            return await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDTO assignRoleDTO)
        {
            _logger.LogInformation($"Assigning role {assignRoleDTO.RoleName} to user {assignRoleDTO.UserId} in repository.");

            var user = await _userManager.FindByIdAsync(assignRoleDTO.UserId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {assignRoleDTO.UserId} not found.");
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var roleExists = await _roleManager.RoleExistsAsync(assignRoleDTO.RoleName);
            if (!roleExists)
            {
                _logger.LogWarning($"Role {assignRoleDTO.RoleName} not found.");
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }

            return await _userManager.AddToRoleAsync(user, assignRoleDTO.RoleName);
        }
    }
}
