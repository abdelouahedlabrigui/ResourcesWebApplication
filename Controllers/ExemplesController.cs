using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Accounting.DeterminationDesCoutsEtDesResultatsCasParticulier;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers
{
    public class ExemplesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExemplesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> DeleteExemplesSeries(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
            }

            var query = await _context.ExempleSeries.FindAsync(id);
            _context.ExempleSeries.Remove(query);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Data deleted from database, for 200 Ok status." });
        }
        [HttpPost]
        public async Task<IActionResult> PostExempleSeries([FromBody] ExempleSerie TitreExemple)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {TitreExemple}, isn't value: {ModelState.IsValid}" });
                }
                _context.ExempleSeries.Add(TitreExemple);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetExempleSeries()
        {
            try
            {
                var query = await _context.ExempleSeries.
                    OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetTitreExempleById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
                }
                var query = await _context.ExempleSeries
                    .Where(w => w.Id ==  id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetExempleSeriesIds()
        {
            try
            {
                var query = await _context.ExempleSeries
                    .OrderByDescending(o => o.Id)
                    .Select(s => new Ids { Id_ = s.Id })          
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> PostInstructions([FromBody] Instruction Instruction)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {Instruction}, isn't value: {ModelState.IsValid}" });
                }
                _context.Instructions.Add(Instruction);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetInstructions()
        {
            try
            {
                var query = await _context.Instructions.
                    OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetInstructionById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
                }
                var query = await _context.Instructions
                    .Where(w => w.Id ==  id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetInstructionsIds()
        {
            try
            {
                var query = await _context.Instructions
                    .OrderByDescending(o => o.Id)
                    .Select(s =>  new Ids { Id_ = s.Id })          
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> PostEnoncees([FromBody] Enoncee Enoncee)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {Enoncee}, isn't value: {ModelState.IsValid}" });
                }
                _context.Enoncees.Add(Enoncee);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetEnoncees()
        {
            try
            {
                var query = await _context.Enoncees.
                    OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetEnonceeById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
                }
                var query = await _context.Enoncees
                    .Where(w => w.Id ==  id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetEnonceesIds()
        {
            try
            {
                var query = await _context.Enoncees
                    .OrderByDescending(o => o.Id)
                    .Select(s =>  new Ids { Id_ = s.Id })          
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> PostConditions([FromBody] Condition Condition)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {Condition}, isn't value: {ModelState.IsValid}" });
                }
                _context.Conditions.Add(Condition);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetConditions()
        {
            try
            {
                var query = await _context.Conditions.
                    OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetConditionById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
                }
                var query = await _context.Conditions
                    .Where(w => w.Id ==  id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetConditionsIds()
        {
            try
            {
                var query = await _context.Conditions
                    .OrderByDescending(o => o.Id)
                    .Select(s =>  new Ids { Id_ = s.Id })          
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> PostObjets([FromBody] Objet Objet)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {Objet}, isn't value: {ModelState.IsValid}" });
                }
                _context.Objets.Add(Objet);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetObjets()
        {
            try
            {
                var query = await _context.Objets.
                    OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetObjetById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
                }
                var query = await _context.Objets
                    .Where(w => w.Id ==  id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetObjetsIds()
        {
            try
            {
                var query = await _context.Objets
                    .OrderByDescending(o => o.Id)
                    .Select(s =>  new Ids { Id_ = s.Id })          
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }

        public async Task<IActionResult> PostExemples([FromBody] Exemple exemple)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {exemple}, isn't value: {ModelState.IsValid}" });
                }
                _context.Exemples.Add(exemple);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetExemples()
        {
            try
            {
                var query = await _context.Exemples.
                    OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetExempleById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new { Message = $"Id is null or empty: {id.ToString()}" });
                }
                var query = await _context.Exemples
                    .Where(w => w.Id ==  id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        public async Task<IActionResult> GetExemplesIds()
        {
            try
            {
                var query = await _context.Exemples
                    .OrderByDescending(o => o.Id)
                    .Select(s => new { ExempleId = s.Id })          
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count: {query.Count()}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }

        // GET: Exemples
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exemples.ToListAsync());
        }

        // GET: Exemples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exemple = await _context.Exemples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exemple == null)
            {
                return NotFound();
            }

            return View(exemple);
        }

        // GET: Exemples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exemples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Instructions,CreatedAT")] Exemple exemple)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exemple);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exemple);
        }

        // GET: Exemples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exemple = await _context.Exemples.FindAsync(id);
            if (exemple == null)
            {
                return NotFound();
            }
            return View(exemple);
        }

        // POST: Exemples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Instructions,CreatedAT")] Exemple exemple)
        {
            if (id != exemple.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exemple);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExempleExists(exemple.Id))
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
            return View(exemple);
        }

        // GET: Exemples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exemple = await _context.Exemples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exemple == null)
            {
                return NotFound();
            }

            return View(exemple);
        }

        // POST: Exemples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exemple = await _context.Exemples.FindAsync(id);
            _context.Exemples.Remove(exemple);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExempleExists(int id)
        {
            return _context.Exemples.Any(e => e.Id == id);
        }
    }
}
