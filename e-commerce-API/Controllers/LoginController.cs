using e_commerce_API.Data;
using e_commerce_API.Model.Users;
using e_commerce_API.Model.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace e_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly APIContext dbContext;
        private readonly IConfiguration configuration;

        public LoginController(APIContext context, IConfiguration config)
        {
            dbContext = context;
            configuration = config;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Check if username or email already exists
                if (await dbContext.Users.AnyAsync(u => u.Username == request.Username))
                {
                    return BadRequest(new { message = "Username already exists" });
                }

                if (await dbContext.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return BadRequest(new { message = "Email already exists" });
                }

                var now = DateTime.UtcNow;
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = request.Role ?? "customer",
                    CreatedAt = now,
                    UpdatedAt = now
                };

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new  {token,user });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Username", user.Username),
                new Claim("Role", user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
