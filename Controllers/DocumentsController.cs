using InvestigationSupportSystem.Data;
using InvestigationSupportSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestigationSupportSystem.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var docs = _context.Documents.Include(d => d.Case).ToList();
            return View(docs);
        }

        public IActionResult Upload() => View();

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int caseId, string description)
        {
            if (file != null && file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var doc = new Document
                {
                    FileName = file.FileName,
                    Content = ms.ToArray(),
                    CaseId = caseId,
                    Description = description
                };
                _context.Documents.Add(doc);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}