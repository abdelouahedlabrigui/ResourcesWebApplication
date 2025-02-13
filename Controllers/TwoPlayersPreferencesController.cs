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
    public class TwoPlayersPreferencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TwoPlayersPreferencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetPlayersID()
        {
            return Json(await _context.Players.Select(m => m.Id).ToListAsync());
        }

        public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players.OrderByDescending(d => d.Id).Take(10).Distinct().ToListAsync();
            return Json(players);
        }
        // GET: TwoPlayersPreferences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Preferences.ToListAsync());
        }

        // GET: TwoPlayersPreferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preferences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preference == null)
            {
                return NotFound();
            }

            return View(preference);
        }

        // GET: TwoPlayersPreferences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TwoPlayersPreferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FthPlayerID,SndPlayerID,FthPlayerPreference,SndPlayerPreference")] Preference preference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preference);
        }
        [HttpPost]
        public async Task<IActionResult> PostPreferences([FromBody] List<Preference> preferences)
        {
            try
            {
                foreach (var item in preferences)
                {
                    Preference preference = new Preference()
                    {
                        FthPlayerID = item.FthPlayerID,
                        SndPlayerID = item.SndPlayerID,
                        FthPlayerPreference = item.FthPlayerPreference,
                        SndPlayerPreference = item.SndPlayerPreference
                    };
                    _context.Add(preference);
                }
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // GET: TwoPlayersPreferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preferences.FindAsync(id);
            if (preference == null)
            {
                return NotFound();
            }
            return View(preference);
        }

        // POST: TwoPlayersPreferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FthPlayerID,SndPlayerID,FthPlayerPreference,SndPlayerPreference")] Preference preference)
        {
            if (id != preference.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferenceExists(preference.Id))
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
            return View(preference);
        }

        // GET: TwoPlayersPreferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preferences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preference == null)
            {
                return NotFound();
            }

            return View(preference);
        }

        // POST: TwoPlayersPreferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preference = await _context.Preferences.FindAsync(id);
            _context.Preferences.Remove(preference);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreferenceExists(int id)
        {
            return _context.Preferences.Any(e => e.Id == id);
        }
    }
}
