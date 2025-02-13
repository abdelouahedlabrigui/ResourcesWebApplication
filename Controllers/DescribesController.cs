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
    public class DescribesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescribesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetDatasetNames()
        {
            try
            {
                var datasetNames = await _context.Describes.Select(d => d.DatasetName).Distinct().ToListAsync();
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
        public async Task<IActionResult> GetClassifiedData(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName.ToString()))
                {
                    return NotFound();   
                }
                var plotByColumnData = await _context.PlotByColumns.Where(d => d.DatasetName == datasetName).ToListAsync();
                List<Describe> data = new List<Describe>();
                foreach (var item in plotByColumnData)
                {                
                    var description = await _context.Describes.Where(d => d.DatasetName == datasetName && d.Column.Contains(item.Column)).ToListAsync();
                    foreach (var indicator in description)
                    {
                        Describe descriptionData = new Describe()
                        {
                            Column = indicator.Column,
                            Mean = indicator.Mean,
                            Std = indicator.Std,
                            Min = indicator.Min,
                            Max = indicator.Max,
                        };
                        data.Add(descriptionData);
                    }
                }
                List<object> encodingPlots = new List<object>();
                for (var i = 0; i < data.Count; i++)
                {
                    var selectedData = new {
                        DatasetName = data[i].DatasetName,
                        Column = data[i].Column,
                        Mean = data[i].Mean,
                        Std = data[i].Std,
                        Min = data[i].Min,
                        Max = data[i].Max,
                        Encoding = plotByColumnData[i].Encoding
                    };
                    encodingPlots.Add(selectedData);
                }
                if (encodingPlots.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(encodingPlots);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult ClassifiedData()
        {
            return View();
        }
        public async Task<IActionResult> GetDescribedDataset(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    return NotFound();
                }
                var data = await _context.Describes.Where(d => d.DatasetName == datasetName).ToListAsync();
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
        // GET: Describes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Describes.ToListAsync());
        }

        // GET: Describes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var describe = await _context.Describes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (describe == null)
            {
                return NotFound();
            }

            return View(describe);
        }

        // GET: Describes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Describes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatasetName,Column,Count,Mean,Std,Min,TwentyFivePercent,FiftyPercent,SeventyFivePercent,Max,CreatedAT")] Describe describe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(describe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(describe);
        }

        // GET: Describes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var describe = await _context.Describes.FindAsync(id);
            if (describe == null)
            {
                return NotFound();
            }
            return View(describe);
        }

        // POST: Describes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatasetName,Column,Count,Mean,Std,Min,TwentyFivePercent,FiftyPercent,SeventyFivePercent,Max,CreatedAT")] Describe describe)
        {
            if (id != describe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(describe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescribeExists(describe.Id))
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
            return View(describe);
        }

        // GET: Describes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var describe = await _context.Describes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (describe == null)
            {
                return NotFound();
            }

            return View(describe);
        }

        // POST: Describes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var describe = await _context.Describes.FindAsync(id);
            _context.Describes.Remove(describe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescribeExists(int id)
        {
            return _context.Describes.Any(e => e.Id == id);
        }
    }
}
