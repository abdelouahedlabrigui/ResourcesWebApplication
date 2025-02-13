using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Tenses;

namespace ResourcesWebApplication.Controllers
{
    public class TensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tenses.Take(8).OrderByDescending(d => d.Id).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> PostTensesData([FromBody] List<Tense> Tenses)
        {
            try
            {
                foreach (var item in Tenses)
                {
                    Tense tense = new Tense()
                    {
                        Name = item.Name,
                        CreatedAT = item.CreatedAT
                    };
                    _context.Add(tense);
                    await _context.SaveChangesAsync();
                }
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        // GET: Tenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tense = await _context.Tenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tense == null)
            {
                return NotFound();
            }

            return View(tense);
        }

        // GET: Tenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAT")] Tense tense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tense);
        }

        // GET: Tenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tense = await _context.Tenses.FindAsync(id);
            if (tense == null)
            {
                return NotFound();
            }
            return View(tense);
        }

        // POST: Tenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedAT")] Tense tense)
        {
            if (id != tense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenseExists(tense.Id))
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
            return View(tense);
        }

        // GET: Tenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tense = await _context.Tenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tense == null)
            {
                return NotFound();
            }

            return View(tense);
        }

        // POST: Tenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tense = await _context.Tenses.FindAsync(id);
            _context.Tenses.Remove(tense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenseExists(int id)
        {
            return _context.Tenses.Any(e => e.Id == id);
        }
    }
}
