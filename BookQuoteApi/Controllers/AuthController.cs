using BookQuoteApi.Dtos;
using BookQuoteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookQuoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            // Basic validation rules enforced on the server
            if (string.IsNullOrWhiteSpace(dto.Username))
            {
                return BadRequest("Username must not be empty.");
            }

            if (dto.Password.Length < 8)
            {
                return BadRequest("Password must be at least 8 characters.");
            }

            if (!dto.Password.Any(char.IsUpper))
            {
                return BadRequest("Password must contain at least one uppercase letter.");
            }

            if (!dto.Password.Any(char.IsLower))
            {
                return BadRequest("Password must contain at least one lowercase letter.");
            }

            if (!dto.Password.Any(char.IsDigit))
            {
                return BadRequest("Password must contain at least one number.");
            }

            if (!dto.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return BadRequest("Password must contain at least one special character.");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            if (_authService.UsernameExists(dto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = _authService.Register(
                dto.Username,
                dto.Password);

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var token = _authService.Login(
                dto.Username,
                dto.Password);

            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new
            {
                token = token
            });
        }
    }
}
