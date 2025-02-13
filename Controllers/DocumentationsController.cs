using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Documents;
using ResourcesWebApplication.Models.Jira;
using ResourcesWebApplication.Models.Trees;

namespace ResourcesWebApplication.Controllers
{
    public class DocumentationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAllVersionInfos()
        {
            try
            {
                var query = await _context.VersionInfos
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });                    
                }
                _context.VersionInfos.RemoveRange(query);
                await _context.SaveChangesAsync();
                return Ok(new { Message = $"Data deleted successfully" }); 
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAllInitialMetadatDetail()
        {
            try
            {
                var query = await _context.initialMetadatDetails
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });                    
                }
                _context.initialMetadatDetails.RemoveRange(query);
                await _context.SaveChangesAsync();
                return Ok(new { Message = $"Data deleted successfully" }); 
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostVersionInfo([FromBody] VersionInfo versionInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state is not valid: {ModelState.IsValid}" });
                }
                var query = await _context.VersionInfos
                    .Where(w => w.FileName == versionInfo.FileName)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    _context.VersionInfos.Add(versionInfo);
                    await _context.SaveChangesAsync();  
                    return Ok(new { Message = "Version Info Posted." });                  
                }
                
                return Ok(new { Message = "No Version Info Posted." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVersionInfoDirectoryMetadata()
        {
            try
            {
                var queryVersionInfo = await _context.VersionInfos
                    .Where(w => w.FileName.Contains("Caculus"))
                    .OrderByDescending(o => o.Id)
                    .Select(s => new { s.Id, s.FileName})
                    .ToListAsync();
                var queryDirectoryDetails = await _context.initialMetadatDetails
                    .Where(w => w.FullName.Contains("Calculus"))
                    .OrderByDescending(o => o.Id)
                    .Select(s => new { s.Id, s.FullName, s.Length, s.LastAccessTime, s.LastWriteTime })
                    .ToListAsync();
                    
                var queries = new {
                    VersionInfoData = queryVersionInfo,
                    DirectoryDetails = queryDirectoryDetails
                };
                return Ok(queries);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetVersionInfo()
        {
            try
            {
                var query = await _context.VersionInfos
                    .Where(w => w.FileName != "None")
                    .OrderByDescending(o => o.Id)
                    .Select(s => new { s.Id, s.FileName})
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });                    
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostInitialMetadataDetail([FromBody] InitialMetadatDetail detail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state is not valid: {ModelState.IsValid}" });
                }
                var query = await _context.initialMetadatDetails
                    .Where(w => w.Name == detail.Name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    _context.initialMetadatDetails.Add(detail);
                    await _context.SaveChangesAsync();  
                    return Ok(new { Message = "Directory Metadata Details Posted." });                  
                }
                return Ok(new { Message = "No Directory Metadata Details Posted." });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInitialMetadataDetail()
        {
            try
            {
                var query = await _context.initialMetadatDetails
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });                    
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message }); 
            }
        }
        
        // GET: Documentations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Documentations.OrderByDescending(d => d.Id).ToListAsync());
        }
        public async Task<IActionResult> ForgeHelpIndex()
        {
            return View(await _context.ForgeHelps.OrderByDescending(d => d.Id).ToListAsync());
        }
        // GET: Documentations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        // GET: Documentations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documentations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,CreatedAT")] Documentation documentation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentation);
        }

        // GET: Documentations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }
            return View(documentation);
        }

        // POST: Documentations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,CreatedAT")] Documentation documentation)
        {
            if (id != documentation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentationExists(documentation.Id))
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
            return View(documentation);
        }

        // GET: Documentations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        // POST: Documentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentation = await _context.Documentations.FindAsync(id);
            _context.Documentations.Remove(documentation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentationExists(int id)
        {
            return _context.Documentations.Any(e => e.Id == id);
        }
    }
}
