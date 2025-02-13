using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Documents;

namespace ResourcesWebApplication.Controllers
{
    public class MarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Marks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Marks.Take(15).OrderByDescending(_id => _id.Id).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> SearchByTitle(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return NotFound();
            }
            var documents = await _context.Marks.OrderByDescending(d => d.Id)
                .Where(d => d.DocumentID == Id).ToListAsync();
            
            return View(documents);
        }

        // GET: Marks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .FirstOrDefaultAsync(m => m.Id == id);
            
            int documentID = int.Parse(mark.DocumentID);
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentID);

            var documentMark = new BookMark
            {
                Document = new Document()
                {
                    Id = document.Id,
                    Title = document.Title,
                    Url = document.Url,
                    CreatedAT = document.CreatedAT
                },
                Mark = new Mark()
                {
                    Search = mark.Search,
                    Page = mark.Page,
                    CreatedAT = mark.CreatedAT
                }
            };

            if (mark == null)
            {
                return NotFound();
            }

            return View(documentMark);
        }

        // GET: Marks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentID,Search,Page,CreatedAT")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mark);                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mark);
        }

        [HttpGet]
        public async Task<IActionResult> FetchDocument(int id)
        {
            try
            {
                var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);                
                return Ok(document);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Marks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks.FindAsync(id);
            if (mark == null)
            {
                return NotFound();
            }
            return View(mark);
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DocumentID,Search,Page,CreatedAT")] Mark mark)
        {
            if (id != mark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkExists(mark.Id))
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
            return View(mark);
        }

        // GET: Marks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mark = await _context.Marks.FindAsync(id);
            _context.Marks.Remove(mark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkExists(int id)
        {
            return _context.Marks.Any(e => e.Id == id);
        }
    }
}
