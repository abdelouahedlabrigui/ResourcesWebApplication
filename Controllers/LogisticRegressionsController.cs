using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.MachineLearning;

namespace ResourcesWebApplication.Controllers
{
    public class LogisticRegressionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogisticRegressionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> GetDatasetNames()
        {
            try
            {
                var datasetNames = await _context.ClassificationReports.Select(d => d.DatasetName).Distinct().ToListAsync();
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
        public IActionResult Plots()
        {
            return View();
        }
        
        public async Task<IActionResult> LogisticRegressionPoster(string fileName, string title, string ageValues, string dropColumn, string dropColumns, string dummyColumns, string targetColumn, string agePClassColumns)
        {
            try
            {
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
                        // filename, title, age_values, drop_column, drop_columns, dummy_columns, target_column, age_pclass_columns
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\LogisticRegression\PlotClassificationReport.py """ + fileName + @""" """ + title + @""" """ + ageValues + @""" """ + dropColumn + @""" """ + dropColumns + @""" """ + dummyColumns + @""" """ + targetColumn + @""" """ + agePClassColumns + @"""");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
                LogisticRegression logisticRegression = new LogisticRegression()
                {
                    DatasetName = Path.GetFileName(fileName),
                    FileName = fileName,
                    Title = title,
                    AgeValues = ageValues,
                    DropColumn = dropColumn,
                    DropColumns = dropColumns,
                    DummyColumns = dummyColumns,
                    TargetColumn = targetColumn,
                    AgePClassColumns = agePClassColumns,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                _context.LogisticRegressions.Add(logisticRegression);
                await _context.SaveChangesAsync();
                var data = await _context.ClassificationReports.Where(d => d.DatasetName == logisticRegression.DatasetName).ToListAsync();
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
        // GET: LogisticRegressions
        public async Task<IActionResult> Index()
        {
            return View(await _context.LogisticRegressions.ToListAsync());
        }

        // GET: LogisticRegressions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logisticRegression = await _context.LogisticRegressions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logisticRegression == null)
            {
                return NotFound();
            }

            return View(logisticRegression);
        }

        // GET: LogisticRegressions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogisticRegressions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatasetName,FileName,Title,AgeValues,DropColumn,DropColumns,DummyColumns,TargetColumn,AgePClassColumns,CreatedAT")] LogisticRegression logisticRegression)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logisticRegression);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logisticRegression);
        }

        // GET: LogisticRegressions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logisticRegression = await _context.LogisticRegressions.FindAsync(id);
            if (logisticRegression == null)
            {
                return NotFound();
            }
            return View(logisticRegression);
        }

        // POST: LogisticRegressions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatasetName,FileName,Title,AgeValues,DropColumn,DropColumns,DummyColumns,TargetColumn,AgePClassColumns,CreatedAT")] LogisticRegression logisticRegression)
        {
            if (id != logisticRegression.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logisticRegression);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogisticRegressionExists(logisticRegression.Id))
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
            return View(logisticRegression);
        }

        // GET: LogisticRegressions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logisticRegression = await _context.LogisticRegressions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logisticRegression == null)
            {
                return NotFound();
            }

            return View(logisticRegression);
        }

        // POST: LogisticRegressions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logisticRegression = await _context.LogisticRegressions.FindAsync(id);
            _context.LogisticRegressions.Remove(logisticRegression);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogisticRegressionExists(int id)
        {
            return _context.LogisticRegressions.Any(e => e.Id == id);
        }
    }
}
