using BankingSystemAPI.Models;
using BankingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            try
            {
                _logger.LogInformation("User registration attempt for email: {Email}", model.Email);
                var message = await _accountService.RegisterUserAsync(model);
                _logger.LogInformation("User registration successful for email: {Email}", model.Email);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration for email: {Email}", model.Email);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            try
            {
                _logger.LogInformation("Email verification attempt for User ID: {UserId}", userId);
                var result = await _accountService.VerifyEmailAsync(userId, token);
                
                if (result)
                {
                    _logger.LogInformation("Email verification successful for User ID: {UserId}", userId);
                    return Ok("Email verification successful.");
                }
                
                _logger.LogWarning("Email verification failed for User ID: {UserId}", userId);
                return BadRequest("Email verification failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during email verification for User ID: {UserId}", userId);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthModel model)
        {
            try
            {
                _logger.LogInformation("Login attempt for email: {Email}", model.Email);
                var token = await _accountService.LoginUserAsync(model);
                _logger.LogInformation("User logged in successfully: {Email}", model.Email);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Login failed for email: {Email}", model.Email);
                _logger.LogError(ex, "Error during login for email: {Email}", model.Email);
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                _logger.LogInformation("User logout attempt.");
                await _accountService.LogoutUserAsync();
                _logger.LogInformation("User logged out successfully.");
                return Ok("Logged out");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout.");
                return BadRequest("Logout failed.");
            }
        }
    }
}
