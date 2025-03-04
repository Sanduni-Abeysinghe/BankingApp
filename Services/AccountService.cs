using BankingSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemAPI.Services
{
    public class AccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;

        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            EmailService emailService,
            IConfiguration configuration,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

                public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

public async Task<string> RegisterUserAsync(AuthModel model)
{
    _logger.LogInformation("User registration attempt for email: {Email}", model.Email);

    var existingUser = await _userManager.FindByEmailAsync(model.Email);
    if (existingUser != null)
    {
        _logger.LogWarning("User registration failed: Email already exists - {Email}", model.Email);
        throw new Exception("Email already registered.");
    }

    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
    var result = await _userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded)
    {
        foreach (var error in result.Errors)
        {
            _logger.LogError("Registration error: {Code} - {Description}", error.Code, error.Description);
        }
        throw new Exception("User registration failed. Check logs for details.");
    }

    _logger.LogInformation("User registration successful for email: {Email}", model.Email);

    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    var verificationLink = $"https://yourdomain.com/api/account/verify-email?userId={user.Id}&token={System.Net.WebUtility.UrlEncode(token)}";

    var emailSubject = "Email Verification";
    var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";

    _emailService.SendEmail(user.Email, emailSubject, emailBody);
    _logger.LogInformation("Verification email sent to {Email}", user.Email);

    return "User registered successfully. An email verification link has been sent.";
}


        public async Task<bool> VerifyEmailAsync(string userId, string token)
        {
            _logger.LogInformation("Email verification attempt for User ID: {UserId}", userId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Email verification failed: User not found with ID: {UserId}", userId);
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                _logger.LogInformation("Email verification successful for User ID: {UserId}", userId);
                return true;
            }

            _logger.LogWarning("Email verification failed for User ID: {UserId}", userId);
            return false;
        }

        public async Task<string> LoginUserAsync(AuthModel model)
        {
            _logger.LogInformation("Login attempt for email: {Email}", model.Email);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", model.Email);
                throw new Exception("Invalid login attempt.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);
            
            _logger.LogInformation("User logged in successfully: {Email}", model.Email);
            return GenerateJwtToken(user, roles);
        }

        public async Task LogoutUserAsync()
        {
            _logger.LogInformation("User logout attempt.");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out successfully.");
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            _logger.LogInformation("Generating JWT token for user: {Email}", user.Email);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            _logger.LogInformation("JWT token generated successfully for user: {Email}", user.Email);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
