using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microsMVCNET7.Data;
using microsMVCNET7.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace microsMVCNET7.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        [Authorize]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            List<Report> data;
             Console.WriteLine(User);
            if (!form.IsNullOrEmpty())
            {
                string date = form["date"][0];
                if (!date.IsNullOrEmpty())
                {
                    DateTime d = DateTime.Parse(date);
                    data = await _context.Reports.
                        Where(r => r.Author.Id == User.Identity.GetUserId() && (r.CreatedDate.Month == d.Month && r.CreatedDate.Year == d.Year))
                        .Include(i => i.CategoryValue)
                        .Include(i => i.CategoryValue.Category).ToListAsync();
                    return View(data);
                }
                    
            }
            data = await _context.Reports.Where(r=>r.Author.Id== User.Identity.GetUserId()).Include(i => i.CategoryValue).Include(i=>i.CategoryValue.Category).ToListAsync();
            return View(data);
        }
        public JsonResult GetCats()
        {
            IEnumerable<Category> cats = _context.Categories.ToList();
            IEnumerable<CategoryValue> catVals = _context.CategoryValues.ToList();
            return new JsonResult(new { categories = cats, categoryValues = catVals });
        }
        // GET: Reports/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.Include("Category").Include("CategoryValue")
                .FirstOrDefaultAsync(m => m.Id == id && m.Author.Id == User.Identity.GetUserId());
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        [Authorize]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CategoryValues = await _context.CategoryValues.ToListAsync();
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Price,Description,CreatedDate")] Report report, IFormCollection form)
        {
            string cat = form["Category"][0];
            string subCat = form["CategoryValue"][0];
            CategoryValue categoryValue = _context.CategoryValues.Include("Category").FirstOrDefault(v => v.Id == int.Parse(subCat));
            string currentUserId = User.Identity.GetUserId();
            IdentityUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);
            DateTime date = report.CreatedDate;
        report.CreatedDate= date.ToUniversalTime(); ;
            report.Category = categoryValue.Category;
            report.CategoryValue = categoryValue;
            report.Author = currentUser;

            if (ModelState.IsValid)
            {

                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reports/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Description,CreatedDate")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Reports/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reports'  is null.");
            }
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
          return (_context.Reports?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
