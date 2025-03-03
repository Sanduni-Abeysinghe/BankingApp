using BankingSystemAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services.Interfaces
{
    public interface IRolesService
    {
        IEnumerable<IdentityRole> GetAllRoles();
        Task<IdentityRole> GetRoleByIdAsync(string roleId);
        Task<IdentityResult> CreateRoleAsync(CreateRoleDTO createRoleDTO);
        Task<IdentityResult> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO);
        Task<IdentityResult> DeleteRoleAsync(string roleId);
        Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDTO assignRoleDTO);
    }
}
