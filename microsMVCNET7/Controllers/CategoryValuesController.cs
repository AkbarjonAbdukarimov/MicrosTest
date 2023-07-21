using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using microsMVCNET7.Data;
using microsMVCNET7.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace microsMVCNET7.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryValues
        public async Task<IActionResult> Index()
        {
            var items = await _context.CategoryValues.Include("Category").ToListAsync();
              return _context.CategoryValues != null ? 
                          View(items) :
                          Problem("Entity set 'ApplicationDbContext.CategoryValues'  is null.");
            }

        // GET: CategoryValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryValues == null)
            {
                return NotFound();
            }

            var categoryValue = await _context.CategoryValues.Include("Category")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryValue == null)
            {
                return NotFound();
            }

            return View(categoryValue);
        }

        // GET: CategoryValues/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        // POST: CategoryValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value")] CategoryValue categoryValue,IFormCollection form)
        {
            string cat = form["Category"][0];
            if (cat!="Select")
            {

                Category category =  _context.Categories.Find(int.Parse(cat));
                categoryValue.Category = category;
            }
            
            if (ModelState.IsValid)
            { 
               
                _context.Add(categoryValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        // GET: CategoryValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryValues == null)
            {
                return NotFound();
            }

            var categoryValue = await _context.CategoryValues.FindAsync(id);
            if (categoryValue == null)
            {
                return NotFound();
            }
            return View(categoryValue);
        }

        // POST: CategoryValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value")] CategoryValue categoryValue)
        {
            if (id != categoryValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryValueExists(categoryValue.Id))
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
            return View(categoryValue);
        }

        // GET: CategoryValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryValues == null)
            {
                return NotFound();
            }

            var categoryValue = await _context.CategoryValues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryValue == null)
            {
                return NotFound();
            }

            return View(categoryValue);
        }

        // POST: CategoryValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryValues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CategoryValues'  is null.");
            }
            var categoryValue = await _context.CategoryValues.FindAsync(id);
            if (categoryValue != null)
            {
                _context.CategoryValues.Remove(categoryValue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryValueExists(int id)
        {
          return (_context.CategoryValues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
