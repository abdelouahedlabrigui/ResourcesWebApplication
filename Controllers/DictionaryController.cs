using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Dictionaries;

namespace ResourcesWebApplication.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DictionaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dictionary
        public async Task<IActionResult> Index()
        {   
            return View(await _context.Dictionaries.OrderByDescending(d => d.Id).ToListAsync());
        }
        

        // GET: Dictionary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionary = await _context.Dictionaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictionary == null)
            {
                return NotFound();
            }

            return View(dictionary);
        }

        // GET: Dictionary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dictionary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Concept,Definition,Category,CreatedAT")] Dictionary dictionary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dictionary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictionary);
        }

        // GET: Dictionary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionary = await _context.Dictionaries.FindAsync(id);
            if (dictionary == null)
            {
                return NotFound();
            }
            return View(dictionary);
        }

        // POST: Dictionary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Concept,Definition,Category,CreatedAT")] Dictionary dictionary)
        {
            if (id != dictionary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictionary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictionaryExists(dictionary.Id))
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
            return View(dictionary);
        }

        // GET: Dictionary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionary = await _context.Dictionaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictionary == null)
            {
                return NotFound();
            }

            return View(dictionary);
        }

        // POST: Dictionary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dictionary = await _context.Dictionaries.FindAsync(id);
            _context.Dictionaries.Remove(dictionary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictionaryExists(int id)
        {
            return _context.Dictionaries.Any(e => e.Id == id);
        }
    }
}
