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
    public class ReadingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReadingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Readingds
        public async Task<IActionResult> Index()
        {
            // var documentIDs = await _context.Readings.Take(10).OrderByDescending(d => d.Id).Select(d => d.DocumentID).ToListAsync();
            // List<Document> documentList = new List<Document>();
            // foreach (var id in documentIDs)
            // {
            //     var reading = await _context.Readings
            //     .FirstOrDefaultAsync(m => m.Id == id);
            //     if (reading == null)
            //     {
            //         return NotFound();
            //     }
            //     var documentID = int.Parse(reading.DocumentID.ToString());
            //     var documents = await _context.Documents.Take(10).FirstOrDefaultAsync(d => d.Id == documentID);   
            //     Document document = new Document()
            //     {
            //         Title = documents.Title,
            //         Url = documents.Url
            //     };
            //     documentList.Add(document);
            // }
            return View(await _context.Readings.Take(10).OrderByDescending(d => d.Id).ToListAsync());
        }
        // [HttpGet]
        // [Route("GetDocumentNames/{top}")]
        public async Task<IActionResult> FetchDocumentsRead(int top)
        {
            try
            {
                // int top
                if (top == 0 || string.IsNullOrWhiteSpace(top.ToString()))
                {
                    return NotFound();
                }

                var documents = await _context.Documents
                    .Join(_context.Readings,
                        doc => doc.Id,
                        reading => reading.DocumentID,
                        (doc, reading) => new {
                            DocumentIDMeta = reading.DocumentID.ToString(),
                            DocumentTitle = doc.Title.ToString(),
                            DocumentUrl = doc.Url.ToString(),
                            DocumentCreatedAT = doc.CreatedAT.ToString(),
                            ReadingID = reading.Id.ToString(),                            
                            ReadingDateAT = reading.DocumentID.ToString(),
                            ReadingStartAT = reading.StartAT.ToString(),
                            ReadingEndAT = reading.EndAT.ToString(),
                            ReadCreatedAT = reading.CreatedAT.ToString(),
                        }).Take(int.Parse(top.ToString())).OrderByDescending(d => d.ReadingID).ToListAsync();
                
                var timelineAnalysis = documents.Select(item => new
                    {
                        DocumentIDMeta = item.DocumentIDMeta,
                        DocumentTitle = item.DocumentTitle,
                        DocumentUrl = item.DocumentUrl,
                        DocumentCreatedAT = item.DocumentCreatedAT,
                        ReadingID = item.ReadingID,
                        ReadingDateAT = item.ReadingDateAT,
                        ReadingStartAT = item.ReadingStartAT,
                        ReadingEndAT = item.ReadingEndAT,
                        ReadCreatedAT = item.ReadCreatedAT,
                        TimelineDifferenceCreatedAT = DateTime.Parse(item.ReadCreatedAT) - DateTime.Parse(item.DocumentCreatedAT),
                        TimelineDifferenceReading = DateTime.Parse(item.ReadingEndAT) - DateTime.Parse(item.ReadingStartAT)
                    }).ToList();
                return Ok(timelineAnalysis);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> GetReadDocumentsByID()
        {
            try
            {
                var documentIDs = await _context.Readings.Take(10).OrderByDescending(d => d.Id).Select(d => d.DocumentID).ToListAsync();
                if (documentIDs.Count == 0)
                {
                    return NotFound();
                }
                var readings = await _context.Readings.Take(10).OrderByDescending(d => d.Id).ToListAsync();
                for (int i = 0; i < documentIDs.Count; i++)
                {
                    var documents = await _context.Documents.Take(10).Where(d => d.Id == documentIDs[i]).ToListAsync();
                    if (readings[i].Id == documentIDs[i])
                    {
                        var document = new
                        {
                            DocumentID = documents[i].Id,
                            DocumentTitle = documents[i].Title,
                            DocumentUrl = documents[i].Url,
                            DocumentCreatedAT = documents[i].CreatedAT,
                            ReadingID = readings[i].Id,
                            ReadingDateAT = readings[i].DateAT,
                            ReadingStartAT = readings[i].StartAT,
                            ReadingEndAT = readings[i].EndAT,
                            ReadCreatedAT = readings[i].CreatedAT,
                        };   
                    }                   
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> SearchById(int Id)
        {
            if (string.IsNullOrWhiteSpace(Id.ToString()))
            {
                return NotFound();
            }
            var readings = await _context.Readings
                .Where(d => d.DocumentID == Id).ToListAsync();
            
            return View(readings);
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
        [HttpGet]
        public async Task<IActionResult> GetDelays(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }

                var reading = await _context.Readings
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (reading == null)
                {
                    return NotFound();
                }
                var documents = await _context.Documents.Where(d => d.Id == reading.DocumentID)
                    .Join(_context.Readings,
                        doc => doc.Id,
                        reading => reading.DocumentID,
                        (doc, reading) => new {
                            DocumentIDMeta = reading.DocumentID.ToString(),
                            DocumentTitle = doc.Title.ToString(),
                            DocumentUrl = doc.Url.ToString(),
                            DocumentCreatedAT = doc.CreatedAT.ToString(),
                            ReadingID = reading.Id.ToString(),                            
                            ReadingDateAT = reading.DocumentID.ToString(),
                            ReadingStartAT = reading.StartAT.ToString(),
                            ReadingEndAT = reading.EndAT.ToString(),
                            ReadCreatedAT = reading.CreatedAT.ToString(),
                            TimelineDifferenceCreatedAT = DateTime.Parse(reading.CreatedAT) - DateTime.Parse(doc.CreatedAT.ToString()),
                            TimelineDifferenceReading = DateTime.Parse(reading.EndAT) - DateTime.Parse(reading.StartAT)
                        }).OrderByDescending(d => d.ReadingID).ToListAsync();
                if(documents.Count == 0)
                {
                    return NoContent();
                }
                return Ok(documents);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delays(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reading == null)
            {
                return NotFound();
            }
            
            return View(reading);
        }
        // GET: Readingds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reading == null)
            {
                return NotFound();
            }
            var documentID = int.Parse(reading.DocumentID.ToString());
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentID);

            var readingDocument = new ReadingDocument 
            {
                Document = new Document()
                {
                    Id = document.Id,
                    Title = document.Title,
                    Url = document.Url,
                    CreatedAT = document.CreatedAT
                },
                Reading = new Reading()
                {
                    Id = reading.Id,
                    DocumentID = reading.DocumentID,
                    DateAT = reading.DateAT,
                    StartAT = reading.StartAT,
                    EndAT = reading.EndAT,
                    CreatedAT = reading.CreatedAT,
                }
            };

            return View(readingDocument);
        }

        // GET: Readingds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readingds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentID,DateAT,StartAT,EndAT,CreatedAT")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reading);
        }

        // GET: Readingds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }
            return View(reading);
        }

        // POST: Readingds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DocumentID,DateAT,StartAT,EndAT,CreatedAT")] Reading reading)
        {
            if (id != reading.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadingExists(reading.Id))
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
            return View(reading);
        }

        // GET: Readingds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reading = await _context.Readings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reading == null)
            {
                return NotFound();
            }

            return View(reading);
        }

        // POST: Readingds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reading = await _context.Readings.FindAsync(id);
            _context.Readings.Remove(reading);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadingExists(int id)
        {
            return _context.Readings.Any(e => e.Id == id);
        }
    }
}
