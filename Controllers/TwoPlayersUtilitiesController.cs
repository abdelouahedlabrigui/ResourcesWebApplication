using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Games.TwoPlayers;

namespace ResourcesWebApplication.Controllers
{
    public class TwoPlayersUtilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TwoPlayersUtilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TwoPlayersUtilities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilities.ToListAsync());
        }

        // GET: TwoPlayersUtilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utility = await _context.Utilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utility == null)
            {
                return NotFound();
            }

            return View(utility);
        }

        // GET: TwoPlayersUtilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TwoPlayersUtilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FthPlayerID,SndPlayerID,FthPlayerUtility,SndPlayerUtility")] Utility utility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utility);
        }

        // GET: TwoPlayersUtilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utility = await _context.Utilities.FindAsync(id);
            if (utility == null)
            {
                return NotFound();
            }
            return View(utility);
        }

        // POST: TwoPlayersUtilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FthPlayerID,SndPlayerID,FthPlayerUtility,SndPlayerUtility")] Utility utility)
        {
            if (id != utility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilityExists(utility.Id))
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
            return View(utility);
        }

        // GET: TwoPlayersUtilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utility = await _context.Utilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utility == null)
            {
                return NotFound();
            }

            return View(utility);
        }

        // POST: TwoPlayersUtilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utility = await _context.Utilities.FindAsync(id);
            _context.Utilities.Remove(utility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilityExists(int id)
        {
            return _context.Utilities.Any(e => e.Id == id);
        }
    }
}
