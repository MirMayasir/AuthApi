using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if(await _context.Users.AllAsync(u=>u.Username == user.Username))
            {
                return BadRequest("user already exists");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Add(user);
            await _context.SaveChangesAsync();

            return Ok("user registered successfully");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized("Invalid credentials");

            bool isPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!isPassword)
            {
                return Unauthorized("invalid password");
            }

            var token = JwtTokenService.GenerateToken(user.Username);
            return Ok(new { Token = token,
            user.Username
            });
        }
    }
}
