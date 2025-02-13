using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ResourcesWebApplication.Models.Chronometers;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers
{
    public class ChronometersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChronometersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> SELECTTasksByTOP(string top)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(top))
                {
                    return NotFound();
                }
                var tasks = await _context.Chronometers.Take(int.Parse(top)).OrderByDescending(d => d.Id).ToListAsync();
                if (tasks.Count == 0)
                {
                    return NoContent();
                }
                return Ok(tasks);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: Chronometers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chronometers.Take(6).OrderByDescending(_id => _id.Id).ToListAsync());
        }

        public async Task<IActionResult> GetNoteRecord(int id){
            if(string.IsNullOrEmpty(id.ToString())){
                return NotFound();
            }
            var query = await _context.Chronometers.Where(w => w.Id == id).ToListAsync();
            if(query.Count == 0){
                return NoContent();
            }
            return Ok(query);
        }

        // GET: Chronometers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chronometer = await _context.Chronometers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chronometer == null)
            {
                return NotFound();
            }

            return View(chronometer);
        }

        // GET: Chronometers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chronometers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Hour")] Chronometer chronometer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _context.Add(chronometer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } else  if(!ModelState.IsValid) {
                    BadRequest(ModelState);
                }
                return View(chronometer);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostChronometer([FromBody] Chronometer chronometer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                
                _context.Chronometers.Add(chronometer);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostChronometerData([FromBody] List<Chronometer> chronometers)
        {
            try
            {
                foreach (var item in chronometers)
                {
                    Chronometer chronometer = new Chronometer()
                    {
                        Title = item.Title,
                        Hour = item.Hour
                    };
                    _context.Add(chronometer);
                }
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // GET: Chronometers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chronometer = await _context.Chronometers.FindAsync(id);
            if (chronometer == null)
            {
                return NotFound();
            }
            return View(chronometer);
        }

        // POST: Chronometers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Hour")] Chronometer chronometer)
        {
            if (id != chronometer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chronometer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChronometerExists(chronometer.Id))
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
            return View(chronometer);
        }

        // GET: Chronometers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chronometer = await _context.Chronometers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chronometer == null)
            {
                return NotFound();
            }

            return View(chronometer);
        }

        // POST: Chronometers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chronometer = await _context.Chronometers.FindAsync(id);
            _context.Chronometers.Remove(chronometer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChronometerExists(int id)
        {
            return _context.Chronometers.Any(e => e.Id == id);
        }

        public IActionResult LoadHoursForChronometer()
        {
            try
            {
                return Ok(); 
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
