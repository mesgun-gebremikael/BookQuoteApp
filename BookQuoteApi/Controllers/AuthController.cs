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

            if (dto.Username.Length < 3)
            {
                return BadRequest("Username must be at least 3 characters.");
            }

            // Not allow username that is only digits
            if (dto.Username.All(char.IsDigit))
            {
                return BadRequest("Username must include letters (not only numbers).");
            }

            if (dto.Password.Length < 8)
            {
                return BadRequest("Password must be at least 8 characters.");
            }

            // Password complexity: upper, lower, digit, special
            if (!dto.Password.Any(char.IsUpper) || !dto.Password.Any(char.IsLower) || !dto.Password.Any(char.IsDigit) || !dto.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return BadRequest("Password must include uppercase, lowercase, number, and special character.");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return BadRequest("Confirm password does not match.");
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
