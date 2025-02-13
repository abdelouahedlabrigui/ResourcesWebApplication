using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Servers.Users;

namespace ResourcesWebApplication.Controllers
{
    public class HashsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HashsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hashess
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hashes.OrderByDescending(d => d.Id).Take(10).ToListAsync());
        }

        [HttpGet]
        public IActionResult HashPassword(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return NotFound();
                }
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        stringBuilder.Append(b.ToString("x2"));
                    }
                    return Content(stringBuilder.ToString(), "text/plain");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Hashess/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hashes = await _context.Hashes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Hashes == null)
            {
                return NotFound();
            }

            return View(Hashes);
        }

        // GET: Hashess/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hashess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Hashed,CreatedAT")] Hash Hash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Hash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Hash);
        }

        // GET: Hashess/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hashes = await _context.Hashes.FindAsync(id);
            if (Hashes == null)
            {
                return NotFound();
            }
            return View(Hashes);
        }

        // POST: Hashess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,Hashed,CreatedAT")] Hash Hash)
        {
            if (id != Hash.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Hash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HashesExists(Hash.Id))
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
            return View(Hash);
        }

        // GET: Hashess/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Hashes = await _context.Hashes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Hashes == null)
            {
                return NotFound();
            }

            return View(Hashes);
        }

        // POST: Hashess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Hashes = await _context.Hashes.FindAsync(id);
            _context.Hashes.Remove(Hashes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HashesExists(int id)
        {
            return _context.Hashes.Any(e => e.Id == id);
        }
    }
}
