using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Accounting;
using ResourcesWebApplication.Models.Accounting.RegularisationDesStocks;
using ResourcesWebApplication.Models.Accounting.Travaux;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers
{
    public class FinanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinanceController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> DeleteInformations(string chapter, string ex_name)
        {
            try
            {
                if (string.IsNullOrEmpty(chapter) || string.IsNullOrEmpty(ex_name))
                {
                    return Ok(new {Message = "Chapter or Exemple name may be null or empty"});
                }
                var query = await _context.Information_Ex1s
                    .Where(w => w.Chapitre == chapter)
                    .Where(w => w.ExempleName == ex_name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query data count: {query.Count}"});
                }
                _context.Information_Ex1s.RemoveRange(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Deleted {query.Count} rows."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteTravauxAFaire(string chapter, string ex_name)
        {
            try
            {
                if (string.IsNullOrEmpty(chapter) || string.IsNullOrEmpty(ex_name))
                {
                    return Ok(new {Message = "Chapter or Exemple name may be null or empty"});
                }
                var query = await _context.TravauxAFaires
                    .Where(w => w.Chapitre == chapter)
                    .Where(w => w.ExempleName == ex_name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query data count: {query.Count}"});
                }
                _context.TravauxAFaires.RemoveRange(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Deleted {query.Count} rows."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNextInformation(string chapter, string ex_name)
        {
            try
            {
                if (string.IsNullOrEmpty(chapter) || string.IsNullOrEmpty(ex_name))
                {
                    return Ok(new {Message = "Chapter or Exemple name may be null or empty"});
                }
                var query = await _context.Information_Ex1s
                    .Where(w => w.Chapitre == chapter)
                    .Where(w => w.ExempleName == ex_name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query data count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetInstructions(string chapter, string ex_name)
        {
            try
            {
                if (string.IsNullOrEmpty(chapter) || string.IsNullOrEmpty(ex_name))
                {
                    return Ok(new {Message = "Chapter or Exemple name may be null or empty"});
                }
                var query = await _context.TravauxAFaires
                    .Where(w => w.Chapitre == chapter)
                    .Where(w => w.ExempleName == ex_name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query data count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostInformation_ex1([FromBody] Information_ex1 information)
        {
            try
            {
                if (information == null)
                {
                    return Ok(new {Message = "Object Model is null"});
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    _context.Information_Ex1s.Add(information);
                    await _context.SaveChangesAsync();
                }
                return Ok(new {Message = "Data saved successfully."});
                
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostTravauxAFaires([FromBody] TravauxAFaire afaire)
        {
            try
            {
                if (afaire == null)
                {
                    return Ok(new {Message = "Object Model is null"});
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    _context.TravauxAFaires.Add(afaire);
                    await _context.SaveChangesAsync();
                }
                return Ok(new {Message = "Data saved successfully."});
                
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Finance
        public async Task<IActionResult> Index()
        {
            return View(await _context.Information_Ex1s.ToListAsync());
        }

        // GET: Finance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information_ex1 = await _context.Information_Ex1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (information_ex1 == null)
            {
                return NotFound();
            }

            return View(information_ex1);
        }

        // GET: Finance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Finance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Entreprise,DateAu,Elements,Marchandises,MatieresPremieres,ProduitFinis")] Information_ex1 information_ex1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(information_ex1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(information_ex1);
        }

        // GET: Finance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information_ex1 = await _context.Information_Ex1s.FindAsync(id);
            if (information_ex1 == null)
            {
                return NotFound();
            }
            return View(information_ex1);
        }

        // POST: Finance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Entreprise,DateAu,Elements,Marchandises,MatieresPremieres,ProduitFinis")] Information_ex1 information_ex1)
        {
            if (id != information_ex1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(information_ex1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Information_ex1Exists(information_ex1.Id))
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
            return View(information_ex1);
        }

        // GET: Finance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information_ex1 = await _context.Information_Ex1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (information_ex1 == null)
            {
                return NotFound();
            }

            return View(information_ex1);
        }

        // POST: Finance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var information_ex1 = await _context.Information_Ex1s.FindAsync(id);
            _context.Information_Ex1s.Remove(information_ex1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Information_ex1Exists(int id)
        {
            return _context.Information_Ex1s.Any(e => e.Id == id);
        }
    }
}
