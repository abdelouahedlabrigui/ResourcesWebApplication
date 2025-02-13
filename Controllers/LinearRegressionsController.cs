using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.MachineLearning;

namespace ResourcesWebApplication.Controllers
{
    public class LinearRegressionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinearRegressionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetDatasetNames()
        {
            try
            {
                var datasetNames = await _context.Coefficients.Select(d => d.DatasetName).Distinct().ToListAsync();
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
        public async Task<IActionResult> GetCoefficient(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    return NotFound();
                }
                var data = await _context.Coefficients.Where(d => d.DatasetName == datasetName).ToListAsync();
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
        public IActionResult Plots()
        {
            try
            {
                return View();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetPlotId()
        {
            try
            {
                var id = await _context.Plots.OrderByDescending(d => d.Id).Select(d => d.Id).ToListAsync();
                if (id.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetPlotIdByColumn()
        {
            try
            {
                var id = await _context.PlotByColumns.OrderByDescending(d => d.Id).Select(d => d.Id).ToListAsync();
                if (id.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetPlotByColumn(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();   
                }
                var data = await _context.PlotByColumns.Where(d => d.Id == id).ToListAsync();
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
        public async Task<IActionResult> GetPlots(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();   
                }
                var data = await _context.Plots.Where(d => d.Id == id).ToListAsync(); 
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
        public async Task<IActionResult> CoefficientAndPlotsPosts(string filename, string datasetName, string xlabel,  string ylabel, string title)
        {
            try
            {
                string coefficientEndPoint = "http://localhost:3000/MachineLearning/CoefficientPost";
                string pairplotEndPoint = "http://localhost:3000/MachineLearning/PlotPost";
                string plotByColumnEndPoint = "http://localhost:3000/MachineLearning/PlotByColumnPost";
                if (string.IsNullOrWhiteSpace(filename) || string.IsNullOrWhiteSpace(datasetName) || string.IsNullOrWhiteSpace(ylabel) || string.IsNullOrWhiteSpace(xlabel) || string.IsNullOrWhiteSpace(title))
                {
                    return NotFound();
                }
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\CoefficientPoster.py" + " " + coefficientEndPoint + " " + pairplotEndPoint + " " + plotByColumnEndPoint + " " + filename.Replace(@"\", "\\") + " " + datasetName + " " + xlabel + " " + ylabel + " " + title.Replace(" ", "_"));
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
                await Task.Delay(30);
                var data = await _context.Coefficients.Where(d => d.DatasetName == datasetName).ToListAsync();
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

        // GET: LinearRegressions
        public IActionResult Index()
        {
            return View();
        }

        // GET: LinearRegressions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linearRegression = await _context.LinearRegressions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linearRegression == null)
            {
                return NotFound();
            }

            return View(linearRegression);
        }

        // GET: LinearRegressions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LinearRegressions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,coefficientEndPoint,pairplotEndPoint,plotByColumnEndPoint,filename,datasetName,xlabel,ylabel,title")] LinearRegression linearRegression)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linearRegression);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linearRegression);
        }

        // GET: LinearRegressions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linearRegression = await _context.LinearRegressions.FindAsync(id);
            if (linearRegression == null)
            {
                return NotFound();
            }
            return View(linearRegression);
        }

        // POST: LinearRegressions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,coefficientEndPoint,pairplotEndPoint,plotByColumnEndPoint,filename,datasetName,xlabel,ylabel,title")] LinearRegression linearRegression)
        {
            if (id != linearRegression.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linearRegression);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinearRegressionExists(linearRegression.Id))
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
            return View(linearRegression);
        }

        // GET: LinearRegressions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linearRegression = await _context.LinearRegressions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linearRegression == null)
            {
                return NotFound();
            }

            return View(linearRegression);
        }

        // POST: LinearRegressions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linearRegression = await _context.LinearRegressions.FindAsync(id);
            _context.LinearRegressions.Remove(linearRegression);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinearRegressionExists(int id)
        {
            return _context.LinearRegressions.Any(e => e.Id == id);
        }
    }
}
