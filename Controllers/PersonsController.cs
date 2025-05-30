using InvestigationSupportSystem.Data;
using InvestigationSupportSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestigationSupportSystem.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly AppDbContext _context;

        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var persons = _context.Persons.Include(p => p.Case).ToList();
            return View(persons);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
    }
}
