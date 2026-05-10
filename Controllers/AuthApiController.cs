using Microsoft.AspNetCore.Mvc;
using TodoList.Services;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthApiController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthApiController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest request)
        {
            var user = await _authService.Register(request.Email, request.Password);
            if (user == null) return BadRequest("Email already exists");
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            var token = _authService.Login(request.Email, request.Password);
            if (token == null) return Unauthorized("Invalid email or password");
            return Ok(new { token });
        }
    }

    public class AuthRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}