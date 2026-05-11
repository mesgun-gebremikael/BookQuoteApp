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
        public IActionResult Register(LoginDto dto)
        {
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
