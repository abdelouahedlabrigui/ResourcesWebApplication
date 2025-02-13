using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Devops.Wazuh;

namespace ResourcesWebApplication.Controllers
{
    public class WazuhAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WazuhAPIController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetWazuhAPIEndPoints()
        {
            try
            {
                var query = await _context.WazuhEndPoints
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = $"{ex.Message}"});
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostWazuhAPIEndPoints([FromBody] WazuhEndPoint wazuhEndPoint)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return  NotFound();
                }
                _context.WazuhEndPoints.Add(wazuhEndPoint);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data successfully added to wazuh api endpoint entity."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = $"{ex.Message}"});
            }
        }

        // GET: WazuhAPI
        public async Task<IActionResult> Index()
        {
            return View(await _context.WazuhEndPoints.ToListAsync());
        }

        // GET: WazuhAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wazuhEndPoint = await _context.WazuhEndPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wazuhEndPoint == null)
            {
                return NotFound();
            }

            return View(wazuhEndPoint);
        }

        // GET: WazuhAPI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WazuhAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EndPoint,CreatedAT")] WazuhEndPoint wazuhEndPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wazuhEndPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wazuhEndPoint);
        }

        // GET: WazuhAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wazuhEndPoint = await _context.WazuhEndPoints.FindAsync(id);
            if (wazuhEndPoint == null)
            {
                return NotFound();
            }
            return View(wazuhEndPoint);
        }

        // POST: WazuhAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EndPoint,CreatedAT")] WazuhEndPoint wazuhEndPoint)
        {
            if (id != wazuhEndPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wazuhEndPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WazuhEndPointExists(wazuhEndPoint.Id))
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
            return View(wazuhEndPoint);
        }

        // GET: WazuhAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wazuhEndPoint = await _context.WazuhEndPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wazuhEndPoint == null)
            {
                return NotFound();
            }

            return View(wazuhEndPoint);
        }

        // POST: WazuhAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wazuhEndPoint = await _context.WazuhEndPoints.FindAsync(id);
            _context.WazuhEndPoints.Remove(wazuhEndPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WazuhEndPointExists(int id)
        {
            return _context.WazuhEndPoints.Any(e => e.Id == id);
        }
    }
}
