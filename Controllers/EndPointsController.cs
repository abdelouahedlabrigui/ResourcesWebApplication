using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Endpoints;

namespace ResourcesWebApplication.Controllers
{
    public class EndPointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EndPointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EndPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.EndPoints.ToListAsync());
        }

        // GET: EndPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endPoint = await _context.EndPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endPoint == null)
            {
                return NotFound();
            }

            return View(endPoint);
        }

        // GET: EndPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EndPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,CreatedAT")] EndPoint endPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(endPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(endPoint);
        }

        // GET: EndPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endPoint = await _context.EndPoints.FindAsync(id);
            if (endPoint == null)
            {
                return NotFound();
            }
            return View(endPoint);
        }

        // POST: EndPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,CreatedAT")] EndPoint endPoint)
        {
            if (id != endPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EndPointExists(endPoint.Id))
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
            return View(endPoint);
        }

        // GET: EndPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endPoint = await _context.EndPoints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endPoint == null)
            {
                return NotFound();
            }

            return View(endPoint);
        }

        // POST: EndPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endPoint = await _context.EndPoints.FindAsync(id);
            _context.EndPoints.Remove(endPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EndPointExists(int id)
        {
            return _context.EndPoints.Any(e => e.Id == id);
        }
    }
}
