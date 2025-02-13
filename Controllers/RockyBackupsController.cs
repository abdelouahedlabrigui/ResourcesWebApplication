using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Servers.Backups;

namespace ResourcesWebApplication.Controllers
{
    public class RockyBackupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RockyBackupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RockyBackups
        public async Task<IActionResult> Index()
        {
            return View(await _context.RockyBackup.ToListAsync());
        }

        // GET: RockyBackups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backup = await _context.RockyBackup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backup == null)
            {
                return NotFound();
            }

            return View(backup);
        }

        // GET: RockyBackups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RockyBackups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Folder,Name,CreatedAT")] Backup backup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(backup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(backup);
        }

        // GET: RockyBackups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backup = await _context.RockyBackup.FindAsync(id);
            if (backup == null)
            {
                return NotFound();
            }
            return View(backup);
        }

        // POST: RockyBackups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Folder,Name,CreatedAT")] Backup backup)
        {
            if (id != backup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(backup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackupExists(backup.Id))
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
            return View(backup);
        }

        // GET: RockyBackups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backup = await _context.RockyBackup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backup == null)
            {
                return NotFound();
            }

            return View(backup);
        }

        // POST: RockyBackups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var backup = await _context.RockyBackup.FindAsync(id);
            _context.RockyBackup.Remove(backup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackupExists(int id)
        {
            return _context.RockyBackup.Any(e => e.Id == id);
        }
    }
}
