using InvestigationSupportSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestigationSupportSystem.Controllers
{
    [Authorize]
    public class CasesController : Controller
    {
        private readonly AppDbContext _context;

        public CasesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cases = _context.Cases.Include(c => c.Persons).Include(c => c.Documents).ToList();
            return View(cases);
        }
    }
}