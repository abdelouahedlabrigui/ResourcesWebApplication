using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.MachineLearning;

namespace ResourcesWebApplication.Controllers
{
    public class CorrelationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorrelationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetDatasetNames()
        {
            try
            {
                var datasetNames = await _context.Correlations.Select(d => d.DatasetName).Distinct().ToListAsync();
                if (datasetNames.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(datasetNames);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetCorrelatedDataset(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    return NotFound();
                }
                var data = await _context.Correlations.Where(d => d.DatasetName == datasetName).ToListAsync();
                if (data.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: Correlations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Correlations.ToListAsync());
        }

        // GET: Correlations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correlation = await _context.Correlations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correlation == null)
            {
                return NotFound();
            }

            return View(correlation);
        }

        // GET: Correlations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Correlations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatasetName,AvgAreaIncome,AvgAreaHouseAge,AvgAreaNumberofRooms,AvgAreaNumberofBedrooms,AreaPopulation,Price,CreatedAT")] Correlation correlation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correlation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(correlation);
        }

        // GET: Correlations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correlation = await _context.Correlations.FindAsync(id);
            if (correlation == null)
            {
                return NotFound();
            }
            return View(correlation);
        }

        // POST: Correlations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatasetName,AvgAreaIncome,AvgAreaHouseAge,AvgAreaNumberofRooms,AvgAreaNumberofBedrooms,AreaPopulation,Price,CreatedAT")] Correlation correlation)
        {
            if (id != correlation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correlation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrelationExists(correlation.Id))
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
            return View(correlation);
        }

        // GET: Correlations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correlation = await _context.Correlations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correlation == null)
            {
                return NotFound();
            }

            return View(correlation);
        }

        // POST: Correlations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correlation = await _context.Correlations.FindAsync(id);
            _context.Correlations.Remove(correlation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrelationExists(int id)
        {
            return _context.Correlations.Any(e => e.Id == id);
        }
    }
}
