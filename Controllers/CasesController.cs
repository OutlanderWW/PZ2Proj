using InvestigationSupportSystem.Data;
using InvestigationSupportSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public IActionResult Index(bool assigned = false)
        {
            var query = _context.Cases
                .Include(c => c.Persons)
                .Include(c => c.Documents)
                .Include(c => c.OfficerCases)
                    .ThenInclude(oc => oc.Officer)
                .AsQueryable();

            if (assigned)
            {
                var userId = int.Parse(User.FindFirst("UserId").Value);
                query = query.Where(c => c.OfficerCases.Any(oc => oc.OfficerId == userId));
            }

            return View(query.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Case c)//string title, string desc, DateTime sdate, string status)
        {
            // Case c = new Case { Title=title, Description=desc, StartDate=sdate, Status=status };

            _context.Cases.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index");

            // return View();
        }
        public IActionResult Edit(int id)
        {
            var c = _context.Cases.Find(id);
            return View(c);
        }

        [HttpPost]
        public IActionResult Edit(Case c)
        {
            _context.Cases.Update(c);
            _context.SaveChanges();
            return RedirectToAction("Index");

            // return View(c);
        }
        public IActionResult Delete(int id)
        {
            var c = _context.Cases.Find(id);
            if (c != null)
            {
                _context.Cases.Remove(c);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult View(int id)
        {
            var c = _context.Cases.Find(id);
            return View(c);
        }
        // [HttpPost]
        // public IActionResult View(Case c)
        // {
        //     return RedirectToAction("Index");
        // }


    }
}
