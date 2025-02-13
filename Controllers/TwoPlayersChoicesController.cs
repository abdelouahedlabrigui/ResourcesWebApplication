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
    public class TwoPlayersChoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TwoPlayersChoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TwoPlayersChoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Choices.ToListAsync());
        }

        // GET: TwoPlayersChoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // GET: TwoPlayersChoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TwoPlayersChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FthPlayerID,SndPlayerID,Summary")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(choice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(choice);
        }

        // GET: TwoPlayersChoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choices.FindAsync(id);
            if (choice == null)
            {
                return NotFound();
            }
            return View(choice);
        }

        // POST: TwoPlayersChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FthPlayerID,SndPlayerID,Summary")] Choice choice)
        {
            if (id != choice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(choice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(choice.Id))
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
            return View(choice);
        }

        // GET: TwoPlayersChoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // POST: TwoPlayersChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var choice = await _context.Choices.FindAsync(id);
            _context.Choices.Remove(choice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChoiceExists(int id)
        {
            return _context.Choices.Any(e => e.Id == id);
        }
    }
}
