using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Infrastractures;

namespace ResourcesWebApplication.Controllers
{
    public class InfrastructuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfrastructuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Infrastructures
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Infrastractures.OrderByDescending(d => d.Id).ToListAsync());
        }
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _context.Infrastractures.Select(d => d.Category).Take(23).Distinct().ToListAsync();
                if (categories.Count == 0)
                {
                    return NotFound();
                }
                return Ok(categories);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchByCategory(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return NotFound();
                }
                var categories = await _context.Infrastractures
                    .Where(d => d.Category.Contains($"{category}")).ToListAsync();
                
                return View(categories);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    return NotFound();
                }
                var titles = await _context.Infrastractures.OrderByDescending(d => d.Id)
                    .Where(d => d.Title.Contains($"{title}")).ToListAsync();
                if (titles.Count == 0)
                {
                    return NoContent();
                }
                return View(titles);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        // GET: Infrastructures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticalInfrastructure = await _context.Infrastractures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criticalInfrastructure == null)
            {
                return NotFound();
            }

            return View(criticalInfrastructure);
        }

        // GET: Infrastructures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Infrastructures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Title,Document")] CriticalInfrastructure criticalInfrastructure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criticalInfrastructure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criticalInfrastructure);
        }

        // GET: Infrastructures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticalInfrastructure = await _context.Infrastractures.FindAsync(id);
            if (criticalInfrastructure == null)
            {
                return NotFound();
            }
            return View(criticalInfrastructure);
        }

        // POST: Infrastructures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Title,Document")] CriticalInfrastructure criticalInfrastructure)
        {
            if (id != criticalInfrastructure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criticalInfrastructure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriticalInfrastructureExists(criticalInfrastructure.Id))
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
            return View(criticalInfrastructure);
        }

        // GET: Infrastructures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticalInfrastructure = await _context.Infrastractures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criticalInfrastructure == null)
            {
                return NotFound();
            }

            return View(criticalInfrastructure);
        }

        // POST: Infrastructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criticalInfrastructure = await _context.Infrastractures.FindAsync(id);
            _context.Infrastractures.Remove(criticalInfrastructure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticalInfrastructureExists(int id)
        {
            return _context.Infrastractures.Any(e => e.Id == id);
        }
    }
}
