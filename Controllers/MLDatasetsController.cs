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
using ResourcesWebApplication.Models.MachineLearning.Datasets;

namespace ResourcesWebApplication.Controllers
{
    public class MLDatasetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MLDatasetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MLDatasets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Datasets.ToListAsync());
        }

        // GET: MLDatasets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataset = await _context.Datasets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataset == null)
            {
                return NotFound();
            }

            return View(dataset);
        }

        // GET: MLDatasets/Create
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> LoadDatasetFileMetadata(string fullAddress)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fullAddress) || !Path.GetFileName(fullAddress).Contains(".csv"))
                {
                    return NotFound();
                }
                FileInfo fileInfo = null;
                fileInfo = new FileInfo(fullAddress);
                Dataset dataset = new Dataset()
                {
                    FullAddress = fullAddress,
                    Name = fileInfo.Name.ToString(),
                    LastWriteTime = fileInfo.LastWriteTime.ToString(),
                    LastAccessTime = fileInfo.LastAccessTime.ToString(),
                    DirectoryName = fileInfo.DirectoryName.ToString(),
                    Length = fileInfo.Length.ToString(),
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                _context.Datasets.Add(dataset);
                await _context.SaveChangesAsync();

                var data = await _context.Datasets.Where(d => d.Name == dataset.Name).ToListAsync();
                if (data.Count() == 0)
                {
                    return NoContent();
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
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\LogisticRegression\Solutions\Advertising.py """ + dataset.FullAddress + @""" """);
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
                return Ok(dataset);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: MLDatasets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullAddress,Name,LastWriteTime,LastAccessTime,DirectoryName,Length,CreatedAT")] Dataset dataset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataset);
        }

        // GET: MLDatasets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataset = await _context.Datasets.FindAsync(id);
            if (dataset == null)
            {
                return NotFound();
            }
            return View(dataset);
        }

        // POST: MLDatasets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullAddress,Name,LastWriteTime,LastAccessTime,DirectoryName,Length,CreatedAT")] Dataset dataset)
        {
            if (id != dataset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatasetExists(dataset.Id))
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
            return View(dataset);
        }

        // GET: MLDatasets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataset = await _context.Datasets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataset == null)
            {
                return NotFound();
            }

            return View(dataset);
        }

        // POST: MLDatasets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataset = await _context.Datasets.FindAsync(id);
            _context.Datasets.Remove(dataset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatasetExists(int id)
        {
            return _context.Datasets.Any(e => e.Id == id);
        }
    }
}
