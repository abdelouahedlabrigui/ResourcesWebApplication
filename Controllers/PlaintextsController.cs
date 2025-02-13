using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models;
using ResourcesWebApplication.Models.Context;
using System.IO;
using ResourcesWebApplication.Models.Processing;
using ResourcesWebApplication.Library;
using ResourcesWebApplication.Models.Languages;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
// using CSVFile;
using ResourcesWebApplication.Models.LanguageProcessing;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using Newtonsoft.Json;
using ResourcesWebApplication.Models.GenerativeAI;

namespace ResourcesWebApplication.Controllers
{
    public class PlaintextsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaintextsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SpeechQuestions()
        {
            return View();
        }
        // GET Speech
        public IActionResult Speech()
        {
            return View();
        }  
        public IActionResult Tutorial()
        {
            return View();
        }      
        [HttpGet]
        public async Task<IActionResult> PostAnswer(string question)
        {
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-NLP\GenderBasedViolence\PostAnswer.py";
                    

            // Check if the script has already been executed in the current session
            if (HttpContext.Session.GetString("ScriptExecuted") == null)
            {
                // Mark the script as executed in the session
                HttpContext.Session.SetString("ScriptExecuted", "true");

                // Create the file if it doesn't exist
                if (!System.IO.File.Exists(scriptPath + ".executed"))
                {
                    System.IO.File.Create(scriptPath + ".executed").Dispose();
                }
                // Execute the script
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;
                    // startInfo.RedirectStandardOutput = true;
                    // startInfo.RedirectStandardError = true;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                        sw.WriteLine($@"python.exe {scriptPath} ""{question}""");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
            }
            var query = await _context.NlpQuestions.Take(1).OrderByDescending(d => d.Id).ToListAsync();
            return Ok(query);
        }   
        [HttpPost]
        public async Task<IActionResult> PostNlpQuestion([FromBody] NlpQuestion nlpQuestion)
        {
            try
            {
                if (nlpQuestion == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.NlpQuestions.Add(nlpQuestion);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }  
        [HttpGet]
        public async Task<IActionResult> GetNlpQuestion()
        {
            var query = await _context.NlpQuestions.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NotFound();
            }
            return Ok(query[0]);
        }
        // GET: Plaintexts
        public async Task<IActionResult> Index()
        {
            var data = await _context.Plaintexts.OrderByDescending(_id => _id.ID).ToListAsync();
            // if (data.Count == 0)
            // {
            //     return NoContent();
            // }
            // List<PlainTextMetadata> plainTexts = new List<PlainTextMetadata>();
            // FileInfo fileInfo = null;
            // foreach (var item in data)
            // {
            //     if (!item.FullAddress.Contains(".txt") && !item.FullAddress.Contains(@"\"))
            //     {
            //         return NoContent();;
            //     }
            //     fileInfo = new FileInfo(item.FullAddress);
            //     PlainTextMetadata metadata = new PlainTextMetadata
            //     {
            //         ID = item.ID,
            //         FullAddress = fileInfo.FullName.ToString(),
            //         Name = fileInfo.Name.ToString(),
            //         LastWriteTime = fileInfo.LastWriteTime.ToString(),
            //         LastAccessTime = fileInfo.LastAccessTime.ToString(),
            //         Length = fileInfo.Length.ToString(),
            //     };
            //     plainTexts.Add(metadata);
            // }
            return  View(data);
        }

        // GET: Plaintexts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plaintext = await _context.Plaintexts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plaintext == null)
            {
                return NotFound();
            }

            return View(plaintext);
        }

        // GET: Plaintexts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plaintexts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullAddress,Content,CreatedAT")] Plaintext plaintext)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plaintext);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plaintext);
        }

        // GET: Plaintexts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plaintext = await _context.Plaintexts.FindAsync(id);
            if (plaintext == null)
            {
                return NotFound();
            }
            return View(plaintext);
        }

        // POST: Plaintexts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullAddress,Content,CreatedAT")] Plaintext plaintext)
        {
            if (id != plaintext.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plaintext);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaintextExists(plaintext.ID))
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
            return View(plaintext);
        }

        // GET: Plaintexts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plaintext = await _context.Plaintexts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plaintext == null)
            {
                return NotFound();
            }

            return View(plaintext);
        }

        // POST: Plaintexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plaintext = await _context.Plaintexts.FindAsync(id);
            _context.Plaintexts.Remove(plaintext);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaintextExists(int id)
        {
            return _context.Plaintexts.Any(e => e.ID == id);
        }
        [HttpGet]
        public IActionResult LoadFile(string filePath)
        {
            try
            {
                string fileContent = System.IO.File.ReadAllText(filePath);
                return Content(fileContent, "text/plain");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error loading file: {ex.Message}");
            }
        }
        public IActionResult ExecuteEntityRecognitionPyScript(string filename)
        {
            try
            {   
                if (string.IsNullOrEmpty(filename) || !System.IO.File.Exists(filename) || !filename.Contains(".txt"))
                {
                    return NotFound();
                }
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-History-Analysis\History.py";
                
                // Check if the script has already been executed in the current session
                if (HttpContext.Session.GetString("ScriptExecuted") == null)
                {
                    // Mark the script as executed in the session
                    HttpContext.Session.SetString("ScriptExecuted", "true");

                    // Create the file if it doesn't exist
                    if (!System.IO.File.Exists(datasetPath + ".executed"))
                    {
                        System.IO.File.Create(datasetPath + ".executed").Dispose();
                    }

                    // Execute the script
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
                            sw.WriteLine(@"python.exe " + datasetPath  + @" """ + filename + @"""");
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }   
                return RedirectToAction(nameof(Entity));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Entity()
        {
            return View();
        }
        public IActionResult GetSentences(string filename)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filename) || !System.IO.File.Exists(filename))
                {
                    return NotFound();
                }

                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult GetPosCOUNTsBySentence(string jsonFileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jsonFileName) || !jsonFileName.Contains(".json") || !System.IO.File.Exists(jsonFileName))
                {
                    return NotFound();
                }
                string content = System.IO.File.ReadAllText(jsonFileName);
                var data = JsonConvert.DeserializeObject<IEnumerable<PosCount>>(content).ToList();
                var sent = data.Select(d => d.Sentence).Distinct().ToList();
                return Ok(sent);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult GetPosCOUNTsByFeatureName(string jsonFileName, string featureName, string sentence)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jsonFileName) || !jsonFileName.Contains(".json") || !System.IO.File.Exists(jsonFileName) || string.IsNullOrWhiteSpace(featureName))
                {
                    return NotFound();
                }
                string content = System.IO.File.ReadAllText(jsonFileName);
                var data = JsonConvert.DeserializeObject<IEnumerable<PosCount>>(content).ToList();
                switch (featureName)
                {
                    case "POS":
                        var pos = data
                            .Where(d => d.Feature == featureName && d.Sentence == sentence)
                            .GroupBy(d => d.Name) // Group by the name
                            .Select(g => new {
                                Name = g.Key,
                                Count = g.Count() // Count the occurrences of each name
                            })
                            .ToList();
                        return Ok(pos);
                    case "TAG":
                        var tag = data
                            .Where(d => d.Feature == featureName && d.Sentence == sentence)
                            .GroupBy(d => d.Name) // Group by the name
                            .Select(g => new {
                                Name = g.Key,
                                Count = g.Count() // Count the occurrences of each name
                            })
                            .ToList();
                        return Ok(tag);
                    case "DEP":
                        var dep = data
                            .Where(d => d.Feature == featureName && d.Sentence == sentence)
                            .GroupBy(d => d.Name) // Group by the name
                            .Select(g => new {
                                Name = g.Key,
                                Count = g.Count() // Count the occurrences of each name
                            })
                            .ToList();
                        return Ok(dep);
                    case "SHAPE":
                        var shape = data
                            .Where(d => d.Feature == featureName && d.Sentence == sentence)
                            .GroupBy(d => d.Name) // Group by the name
                            .Select(g => new {
                                Name = g.Key,
                                Count = g.Count() // Count the occurrences of each name
                            })
                            .ToList();
                        return Ok(shape);
                    case "ALPHA":
                        var alpha = data
                            .Where(d => d.Feature == featureName && d.Sentence == sentence)
                            .GroupBy(d => d.Name) // Group by the name
                            .Select(g => new {
                                Name = g.Key,
                                Count = g.Count() // Count the occurrences of each name
                            })
                            .ToList();
                        return Ok(alpha);
                    case "STOP":
                        var stop = data
                            .Where(d => d.Feature == featureName && d.Sentence == sentence)
                            .GroupBy(d => d.Name) // Group by the name
                            .Select(g => new {
                                Name = g.Key,
                                Count = g.Count() // Count the occurrences of each name
                            })
                            .ToList();
                        return Ok(stop);
                    default:
                        return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetEntities(string sentence)
        {
            try
            {
                if (string.IsNullOrEmpty(sentence))
                {
                    return NotFound();
                }
                var entities = await _context.EntityRecognitions.Where(d => d.Sentence.Contains($"{sentence}")).ToListAsync();
                
                if (entities.Count == 0)
                {
                    return NoContent();
                }     
                return Ok(entities);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetEntitySentence(string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(filename) || !filename.Contains(".txt"))
                {
                    return NotFound();
                }
                var entities = await _context.EntityRecognitions.Where(d => d.DatasetName == filename).Select(d => d.Sentence).ToListAsync();
                if (entities.Count == 0)
                {
                    return NoContent();
                }     
                return Ok(entities);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public IActionResult FileMetadata(string filePath)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(filePath);
                List<PlainTextMetadata> plainTexts = new List<PlainTextMetadata>();
                FileInfo fileInfo = null;
                foreach (string item in files)
                {
                    fileInfo = new FileInfo(item);
                    PlainTextMetadata metadata = new PlainTextMetadata
                    {
                        FullAddress = fileInfo.FullName.ToString(),
                        Name = fileInfo.Name.ToString(),
                        LastWriteTime = fileInfo.LastWriteTime.ToString(),
                        LastAccessTime = fileInfo.LastAccessTime.ToString(),
                        DirectoryName = fileInfo.DirectoryName.ToString(),
                        Length = fileInfo.Length.ToString(),
                    };
                    plainTexts.Add(metadata);
                }
                return Ok(plainTexts);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error getting file metadata: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetTokens(string text)
        {
            try
            {
                Tokens tokens = new Tokens();
                List<Token> countAndShapes = tokens.GetTokenCountsAndLength(text);
                return Ok(countAndShapes);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error getting tokens: {ex.Message}");
            }
        }
        
    }
}
