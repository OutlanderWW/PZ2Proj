using InvestigationSupportSystem.Data;
using InvestigationSupportSystem.Models;
using InvestigationSupportSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvestigationSupportSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly HashingService _hashing;

        public AccountController(AppDbContext context, HashingService hashing)
        {
            _context = context;
            _hashing = hashing;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string badgeNumber, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.BadgeNumber == badgeNumber);
            if (user != null && _hashing.VerifyPassword(password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Cases");
            }
            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}