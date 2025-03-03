using BankingSystemAPI.DTOs;
using BankingSystemAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRolesService rolesService, ILogger<RolesController> logger)
        {
            _rolesService = rolesService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            _logger.LogInformation("GET request received for all roles.");
            var roles = _rolesService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            _logger.LogInformation($"GET request received for role ID: {roleId}");

            var role = await _rolesService.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO createRoleDTO)
        {
            _logger.LogInformation($"POST request received to create role: {createRoleDTO.RoleName}");

            var result = await _rolesService.CreateRoleAsync(createRoleDTO);
            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO updateRoleDTO)
        {
            _logger.LogInformation($"PUT request received to update role ID: {updateRoleDTO.RoleId}");

            var result = await _rolesService.UpdateRoleAsync(updateRoleDTO);
            if (result.Succeeded)
            {
                return Ok("Role updated successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            _logger.LogInformation($"DELETE request received for role ID: {roleId}");

            var result = await _rolesService.DeleteRoleAsync(roleId);
            if (result.Succeeded)
            {
                return Ok("Role deleted successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleDTO assignRoleDTO)
        {
            _logger.LogInformation($"POST request received to assign role {assignRoleDTO.RoleName} to user {assignRoleDTO.UserId}");

            var result = await _rolesService.AssignRoleToUserAsync(assignRoleDTO);
            if (result.Succeeded)
            {
                return Ok("Role assigned to user successfully.");
            }

            return BadRequest(result.Errors);
        }
    }
}
