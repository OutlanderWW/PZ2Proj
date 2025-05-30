using InvestigationSupportSystem.Data;
using InvestigationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestigationSupportSystem.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.BadgeNumber == request.BadgeNumber);
            if (user != null && new HashingService().VerifyPassword(request.Password, user.PasswordHash))
            {
                return Ok(new { user.Id, user.Token });
            }
            return Unauthorized();
        }

        [HttpGet("cases")]
        public IActionResult GetCases([FromQuery] string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.Token == token);
            if (user == null) return Unauthorized();
            return Ok(_context.Cases.ToList());
        }
    }

    public class LoginRequest
    {
        public string BadgeNumber { get; set; }
        public string Password { get; set; }
    }
}
