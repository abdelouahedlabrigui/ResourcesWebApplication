using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Library.Pings;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Devops;

namespace ResourcesWebApplication.Controllers
{
    public class AuthsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> CredentialsDropDownAccess()
        {
            var query = await _context.Auths                
                .GroupBy(p => new { p.Username, p.Password })
                .Select(d => new { 
                    d.Key.Username, d.Key.Password, Counts = d.Count().ToString()
                })
                .ToListAsync();
            return Ok(query);
        }

        // GET: Auths
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auths.OrderByDescending(d => d.Id).ToListAsync());
        }

        // GET: Auths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // GET: Auths/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetPingResultFromWindowsOs(string vmIP, string agent_ip, string username, string password)
        {
            PingResultMethods pingResultMethods = new PingResultMethods();
            PingResult pingResult = pingResultMethods.GetPingResult(vmIP,agent_ip,username,password);
            return Ok(pingResult);
        }

        // POST: Auths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IPAddress,Username,Password,JwtToken,CreatedAT")] Auth auth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auth);
        }

        // GET: Auths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths.FindAsync(id);
            if (auth == null)
            {
                return NotFound();
            }
            return View(auth);
        }

        // POST: Auths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IPAddress,Username,Password,JwtToken,CreatedAT")] Auth auth)
        {
            if (id != auth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthExists(auth.Id))
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
            return View(auth);
        }

        // GET: Auths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // POST: Auths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auth = await _context.Auths.FindAsync(id);
            _context.Auths.Remove(auth);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthExists(int id)
        {
            return _context.Auths.Any(e => e.Id == id);
        }
    }
}
