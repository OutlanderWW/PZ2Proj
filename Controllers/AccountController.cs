using InvestigationSupportSystem.Data;
using InvestigationSupportSystem.Models;
using InvestigationSupportSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InvestigationSupportSystem.Services;
using Microsoft.EntityFrameworkCore;

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
            Models.User user = _context.Users.FirstOrDefault(u => u.BadgeNumber == badgeNumber);
            if (user != null && _hashing.VerifyPassword(password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString())
                };
                if (user.Role != "Admin")
                {
                    HttpContext.Session.SetString("Admin", "False");
                }
                else { HttpContext.Session.SetString("Admin", "True"); }
                HttpContext.Session.SetString("logged", "true");
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
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult CreateUser() { return View(); }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            var tokenService = new TokenService();
            using var sha = System.Security.Cryptography.SHA256.Create();
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(user.Password);
            var hash = sha.ComputeHash(passwordBytes);
            user.PasswordHash = Convert.ToBase64String(hash);
            user.Password = null;
            user.Token = tokenService.GenerateToken();
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cases");
        }
        public IActionResult Index()
        {
            var users = _context.Users.AsNoTracking().ToList();
            return View(users);
        }


    }
}
