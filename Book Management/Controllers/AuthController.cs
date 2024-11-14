using Book_Management.Data;
using Book_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Book_Management.Services;

namespace Book_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IConfiguration _configuration;  
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(ApplicationDbContext dbContext, IConfiguration configuration, PasswordHasher<User> passwordHasher)
        {
            DbContext = dbContext;
            _configuration = configuration; // Injecting IConfiguration
            _passwordHasher = passwordHasher; // Injecting PasswordHasher
        }





        // POST: api/User/Register
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            // Check if the username already exists
            var existingUser = await DbContext.Users
                .Where(u => u.Username == user.Username)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return Conflict(new { Message = "Username already exists." });
            }

            // Hash the password using ASP.NET Core's PasswordHasher
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            // Save the user in the database
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = user.UserId }, user);
        }

        //login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await DbContext.Users
                .Where(u => u.Username == loginModel.Username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Verify the password using PasswordHasher
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Generate and return JWT token
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            // Define claims for the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                // Additional claims can go here (e.g., roles, email)
            };

            // Retrieve the secret key from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the token settings
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            // Generate and return the JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

