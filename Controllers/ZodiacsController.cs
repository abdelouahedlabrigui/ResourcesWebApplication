using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Astrology;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers
{
    public class ZodiacsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZodiacsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zodiacs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zodiacs.ToListAsync());
        }

        // GET: Zodiacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zodiac = await _context.Zodiacs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zodiac == null)
            {
                return NotFound();
            }

            return View(zodiac);
        }

        // GET: Zodiacs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zodiacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Zodiac zodiac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zodiac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zodiac);
        }

        // GET: Zodiacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zodiac = await _context.Zodiacs.FindAsync(id);
            if (zodiac == null)
            {
                return NotFound();
            }
            return View(zodiac);
        }

        // POST: Zodiacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Zodiac zodiac)
        {
            if (id != zodiac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zodiac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZodiacExists(zodiac.Id))
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
            return View(zodiac);
        }

        // GET: Zodiacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zodiac = await _context.Zodiacs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zodiac == null)
            {
                return NotFound();
            }

            return View(zodiac);
        }

        // POST: Zodiacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zodiac = await _context.Zodiacs.FindAsync(id);
            _context.Zodiacs.Remove(zodiac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZodiacExists(int id)
        {
            return _context.Zodiacs.Any(e => e.Id == id);
        }
    }
}
