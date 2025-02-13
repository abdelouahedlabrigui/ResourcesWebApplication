using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Documents;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
// using ResourcesWebApplication.Models.MachineLearning;
using System.Data.Entity;
using ResourcesWebApplication.Models.MachineLearning;
using ResourcesWebApplication.Models.LanguageProcessing;
using ResourcesWebApplication.Models.Exceptions;
using ResourcesWebApplication.Models.MachineLearning.Datasets;
using ResourcesWebApplication.Models.Documents.Summary;
using ResourcesWebApplication.Models.GenerativeAI;
using ResourcesWebApplication.Models.Careers;

namespace ResourcesWebApplication.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult TimezoneTimer()
        {
            return View();
        }

        public IActionResult MLInfo()
        {
            return View();
        }
        public IActionResult MLDescribe()
        {
            return View();
        }
        public IActionResult RegressionPlot()
        {
            return View();
        }
        public IActionResult LinearRegressionPlot()
        {
            return View();
        }
        public IActionResult Query()
        {
            return View();
        }
        public IActionResult Conceptual()
        {
            return View();
        }
        
        public IActionResult ColumnDescription()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetConcepts()
        {
            try
            {
                var query = await _context.Definitions
                    .OrderByDescending(o => o.Id)
                    .Select(s => new {Concept = s.Concept})
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDefinitions()
        {
            try
            {
                var query = await _context.Definitions.Take(5).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetResponsibilities()
        {
            try
            {
                var query = await _context.Responsibilities.OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostResponsibility([FromBody] Responsibility responsibility)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    var query = await _context.Responsibilities
                        .Where(w => w.Responsibilities == responsibility.Responsibilities)
                        .OrderByDescending(o => o.Id).ToListAsync();
                    if (query.Count == 0)
                    {
                        
                        _context.Responsibilities.Add(responsibility);
                        await _context.SaveChangesAsync();
                    }                    
                }
                return Ok(new {Message = "Data saved successfully."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRequirements()
        {
            try
            {
                var query = await _context.Requirements.OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostRequirement([FromBody] Requirement requirement)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    var query = await _context.Requirements
                        .Where(w => w.Requirements == requirement.Requirements)
                        .OrderByDescending(o => o.Id).ToListAsync();
                    if (query.Count == 0)
                    {
                        
                        _context.Requirements.Add(requirement);
                        await _context.SaveChangesAsync();
                    }                    
                }
                return Ok(new {Message = "Data saved successfully."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            try
            {
                var query = await _context.Jobs.OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostJob([FromBody] Job job)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    var query = await _context.Jobs
                        .Where(w => w.Title == job.Title)
                        .OrderByDescending(o => o.Id).ToListAsync();
                    if (query.Count == 0)
                    {
                        
                        _context.Jobs.Add(job);
                        await _context.SaveChangesAsync();
                    }                    
                }
                return Ok(new {Message = "Data saved successfully."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetExperiences()
        {
            try
            {
                var query = await _context.Experiences.OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostExperience([FromBody] Experience experience)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    var query = await _context.Experiences
                        .Where(w => w.Experiences == experience.Experiences)
                        .OrderByDescending(o => o.Id).ToListAsync();
                    if (query.Count == 0)
                    {
                        
                        _context.Experiences.Add(experience);
                        await _context.SaveChangesAsync();
                    }                    
                }
                return Ok(new {Message = "Data saved successfully."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostDefinition([FromBody] Definition definition)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    var query = await _context.Definitions
                        .Where(w => w.Concept == definition.Concept)
                        .OrderByDescending(o => o.Id).ToListAsync();
                    if (query.Count == 0)
                    {
                        definition.Paragraph.Replace("failed to get console mode for stdout: The handle is invalid. failed to get console mode for stderr: ", " ");
                        _context.Definitions.Add(definition);
                        await _context.SaveChangesAsync();
                    }                    
                }
                return Ok(new {Message = "Data saved successfully."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSummary()
        {
            try
            {
                var query = await _context.Summaries.OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetParts()
        {
            var query = await _context.Parts.ToListAsync();
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetChapters()
        {
            var query = await _context.Chapters.ToListAsync();
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetSections()
        {
            var query = await _context.Sections.ToListAsync();
            return Ok(query);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubsections()
        {
            var query = await _context.Subsections.ToListAsync();
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostSummary([FromBody] Summary summary)
        {
            _context.Summaries.Add(summary);
            await _context.SaveChangesAsync();
            return Ok("Posted data");
        }
        [HttpPost]
        public async Task<IActionResult> PostParts([FromBody] Part part)
        {
            try
            {
                if (part == null)
                {
                    return Ok(new {Message = $"Parts value is null: {part}"});
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    _context.Parts.Add(part);
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
        public async Task<IActionResult> PostChapters([FromBody] Chapter chapter)
        {
            try
            {
                if (chapter == null)
                {
                    return Ok(new {Message = $"Chapter value is null: {chapter}"});
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    _context.Chapters.Add(chapter);
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
        public async Task<IActionResult> PostSections([FromBody] Section section)
        {
            try
            {
                if (section == null)
                {
                    return Ok(new {Message = $"Section value is null: {section}"});
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    _context.Sections.Add(section);
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
        public async Task<IActionResult> PostSubsections([FromBody] Subsection subsection)
        {
            try
            {
                if (subsection == null)
                {
                    return Ok(new {Message = $"Subsection value is null: {subsection}"});
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Invalid Model Object"});
                }
                else 
                {
                    _context.Subsections.Add(subsection);
                    await _context.SaveChangesAsync();
                }
                return Ok(new {Message = "Data saved successfully."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        string controllerName = "LinguisticFeaturesController";
        string loggedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public IActionResult Execute()
        {
            return View();
        }
        public IActionResult SearchByDate()
        {
            return View();
        }
        public IActionResult YearDifference()
        {
            return View();
        }
        public async Task<IActionResult> ExecuteYearDifferencePyScript(string century)
        {
            try
            {   
                if (string.IsNullOrEmpty(century))
                {
                    return NotFound();
                }
                
                string pyScript = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-NLP\Plots\YearDifference.py";
                
                // Check if the script has already been executed in the current session
                if (HttpContext.Session.GetString("ScriptExecuted") == null)
                {
                    // Mark the script as executed in the session
                    HttpContext.Session.SetString("ScriptExecuted", "true");

                    // Create the file if it doesn't exist
                    if (!System.IO.File.Exists(pyScript + ".executed"))
                    {
                        System.IO.File.Create(pyScript + ".executed").Dispose();
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
                            sw.WriteLine(@"python.exe " + pyScript  + @" """ + century + @"""");
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }
                
                await Task.Delay(6);

                string url = $"https://localhost/MachineLearning/GroupByCOUNTYears?century={century}";
                var data = await _context.Plots.Where(d => d.DatasetName == century).ToListAsync(); 
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
        
        public async Task<IActionResult> ExecPythonEnglishLinguisticFeaturesScript(string fullAddress)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fullAddress) || !Path.GetFileName(fullAddress).Contains(".txt"))
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
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-NLP\En_LinguisticFeatures.py """ + fullAddress + @"""");
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
        public async Task<IActionResult> GETPartsOfSpeech()
        {
            try
            {
                var data = await _context.PartOfSpeech.OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }                
                return Ok(data.Count);                
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GETSentiments()
        {
            try
            {
                var data = await _context.NLPSentences.OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }                
                return Ok(data.Count);                
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> GETNounChunks()
        {
            try
            {
                var data = await _context.NounChunks.OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
                {
                    return NoContent();
                }                
                return Ok(data.Count);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        public async Task<IActionResult> GETParseTree()
        {
            try
            {
                var data = await _context.ParseTrees.OrderByDescending(d => d.Id).ToListAsync();       
                if (data.Count == 0)
                {
                    return NoContent();
                }         
                return Ok(data.Count);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> GETLocalTree()
        {
            try
            {
                var data = await _context.LocalTrees.OrderByDescending(d => d.Id).ToListAsync();    
                if (data.Count == 0)
                {
                    return NoContent();
                }            
                return Ok(data.Count);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        public async Task<IActionResult> GETEntityRecognition()
        {
            try
            {
                var data = await _context.Entities.OrderByDescending(d => d.Id).ToListAsync();   
                if (data.Count == 0)
                {
                    return NoContent();
                }             
                return Ok(data.Count);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> ExecuteEntityRecognitionPyScript(string filename)
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
                
                await Task.Delay(4);

                var entities = await _context.EntityRecognitions.Select(d => new {d.Id, d.DatasetName, d.Sentence, d.InformationExtraction}).OrderByDescending(d => d.Id).ToListAsync();
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
        public async Task<IActionResult> GetColumnIDs()
        {
            try
            {
                var columnIDs = await _context.Columns.OrderByDescending(d => d.Id).Select(d => d.Id).ToListAsync();
                if (columnIDs.Count == 0)
                {
                    return NoContent();
                }                 
                return Ok(columnIDs);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetColumnDescriptionGroupByDatasetNames(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName.ToString()))
                {
                    return NotFound();
                }
                var _EntityDescriptions = _context.EntityDescriptions.Where(d => d.DatasetName == datasetName).OrderByDescending(d => d.Id).ToList();
                if (_EntityDescriptions.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(_EntityDescriptions);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetEntityDescriptions()
        {
            try
            {
                var EntityDescriptions = await _context.EntityDescriptions.Take(4).OrderByDescending(d => d.Id).ToListAsync();
                if (EntityDescriptions.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(EntityDescriptions);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> ExecuteColumnDescriptionPyScript(string filename, string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(filename) || !System.IO.File.Exists(filename) || !filename.Contains(".csv") || string.IsNullOrEmpty(datasetName) || !datasetName.Contains(".csv"))
                {
                    return NotFound();
                }
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-Political-Sciences\ColumnDescription\ColumnDescription.py";
                
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
                            sw.WriteLine(@"python.exe " + datasetPath  + @" """ + filename + @" """ + @" """ + datasetName + @"""");
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }

                await Task.Delay(3);
                
                var EntityDescriptions = await _context.EntityDescriptions.Where(d => d.DatasetName == datasetName).Select(d => new { d.Id, d.Column, d.Description}).OrderByDescending(d => d.Id).ToListAsync();
                if (EntityDescriptions.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(EntityDescriptions);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> QueryPoster(string url)
        {
            try
            {
                string filePath = @"" + url;
                if (!filePath.Contains(".sql"))
                {
                    return NotFound();
                }
                
                FileInfo fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                {
                    return NotFound();
                }

                string query = System.IO.File.ReadAllText(filePath).Replace('\n', ' ');
                
                QueryData queryFileData = new QueryData()
                {
                    Category = "T-SQL Query",
                    Title = fileInfo.Name.Replace('_', ' '),
                    Name = fileInfo.Name,
                    LastWriteTime = fileInfo.LastWriteTime.ToString(),
                    LastAccessTime = fileInfo.LastAccessTime.ToString(),
                    DirectoryName = fileInfo.DirectoryName,
                    Length = fileInfo.Length.ToString(),
                    Query = query.Replace('\r', ' ').Replace('\t', ' '),
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                _context.QueryDataStores.Add(queryFileData);
                await _context.SaveChangesAsync();

                var data = await _context.QueryDataStores
                    .OrderByDescending(d => d.Id)
                    .Take(10)
                    .ToListAsync();

                return Ok(data);
            }
            catch (System.Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred while processing the request.{ex.Message}");
            }
        }
        public async Task<IActionResult> GetDocumentIndicators(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return NotFound();
                }
                var indicators = await _context.Indicators.OrderByDescending(d => d.Id).Where(d => d.DatasetName == datasetName).Distinct().ToListAsync();
                if (indicators.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(indicators);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> ExecuteColumnsPyScript(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName) || !System.IO.File.Exists(datasetName) || !datasetName.Contains(".csv"))
                {
                    return NotFound();
                }
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-Political-Sciences\Column\Column.py";
                
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
                            sw.WriteLine(@"python.exe " + datasetPath + @" """ + datasetName + @""" """);
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }

                await Task.Delay(3);

                var columns = await _context.Columns.Where(d => d.DatasetName == Path.GetFileName(datasetName)).OrderByDescending(d => d.Id).Select(d => new { d.Column, d.CreatedAT}).Distinct().ToListAsync();
                if (columns.Count == 0)
                {
                    return NoContent();
                }
                return Ok(columns);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> ExecuteLinearRegressionPlotPyScript(string datasetName, string title, string xColumn, string yColumn)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(xColumn) || string.IsNullOrEmpty(yColumn))
                {
                    return NotFound();
                }
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-Economic-Analysis\Plot\RegressionPlotWithLinearRegression.py";
                
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
                            sw.WriteLine(@"python.exe " + datasetPath + @" """ + datasetName + @""" """ + title.Replace(' ', '_') + @""" """ + xColumn.Replace(' ', '_') + @""" """ + yColumn.Replace(' ', '_') + @"""");
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }

                await Task.Delay(3);

                // var infos = await _context.Infos.OrderByDescending(d => d.Id).Where(d => d.DatasetName == Path.GetFileName(datasetName)).ToListAsync();
                var plot = await _context.Plots
                    .Where(d => d.DatasetName == Path.GetFileName(datasetName))
                    .OrderByDescending(d => d.Id) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (plot.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(plot);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetRegressionPlotyDatasetNames()
        {
            try
            {
                var describes = await _context.Infos.OrderByDescending(d => d.Id).Select(d => d.DatasetName).Distinct().ToListAsync();
                if (describes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(describes);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetRegressionPlot(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return NotFound();
                }
                var fullAddress = await _context.Datasets.Where(d => d.FullAddress.Contains($"{datasetName}")).Select(d => d.FullAddress).ToListAsync();
                if (fullAddress.Count == 0)
                {
                    return NoContent();
                }
                return Ok(fullAddress);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDatasetFullAddress(string filename)
        {
            try
            {
                var fullAddress = await _context.Datasets.Take(1).Where(d => d.FullAddress.Contains($"{filename}")).Select(d => d.FullAddress).ToListAsync();
                return Ok(fullAddress);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetPlotByDatasetName(string datasetName)
        {
            try
            {
                var plot = await _context.Plots
                    .Where(d => d.DatasetName == Path.GetFileName(datasetName))
                    .OrderByDescending(d => d.Id) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (plot.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(plot);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> ExecuteRegressionPlotPyScript(string datasetName, string title, string fthColumn, string sndColumn)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(fthColumn) || string.IsNullOrEmpty(sndColumn))
                {
                    return NotFound();
                }
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-Economic-Analysis\Plot\RegressionPlot.py";
                
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
                            sw.WriteLine(@"python.exe " + datasetPath + @" """ + datasetName + @""" """ + title.Replace(' ', '_') + @""" """ + fthColumn.Replace(' ', '_') + @""" """ + sndColumn.Replace(' ', '_') + @"""");
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }

                await Task.Delay(3);

                // var infos = await _context.Infos.OrderByDescending(d => d.Id).Where(d => d.DatasetName == Path.GetFileName(datasetName)).ToListAsync();
                var plot = await _context.Plots
                    .Where(d => d.DatasetName == Path.GetFileName(datasetName))
                    .OrderByDescending(d => d.Id) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (plot.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(plot);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> ExecuteDescriptionPyScript(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return NotFound();
                }
                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-Economic-Analysis\Describe\Describe.py";
                
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
                            sw.WriteLine(@"python.exe " + datasetPath + @" """ + datasetName + @""" """);
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }

                await Task.Delay(3);

                // var infos = await _context.Infos.OrderByDescending(d => d.Id).Where(d => d.DatasetName == Path.GetFileName(datasetName)).ToListAsync();
                var description = await _context.Describes
                    .Where(d => d.DatasetName == Path.GetFileName(datasetName))
                    .OrderByDescending(d => d.Id) // Assuming Id is a unique identifier
                    .Select(d => new { d.DatasetName, d.Column, d.Count, d.Mean, d.Std, d.Min, d.TwentyFivePercent, d.FiftyPercent, d.SeventyFivePercent, d.Max, d.CreatedAT }) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (description.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(description);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDocumentDescriptionDatasetNames()
        {
            try
            {
                var describes = await _context.Describes.OrderByDescending(d => d.Id).Select(d => d.DatasetName).Distinct().ToListAsync();
                if (describes.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(describes);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDocumentDescription(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return NotFound();
                }
                var description = await _context.Describes
                    .Where(d => d.DatasetName == Path.GetFileName(datasetName))
                    .OrderByDescending(d => d.Id) // Assuming Id is a unique identifier
                    .Select(d => new { d.DatasetName, d.Column, d.Count, d.Mean, d.Std, d.Min, d.TwentyFivePercent, d.FiftyPercent, d.SeventyFivePercent, d.Max, d.CreatedAT }) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (description.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(description);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDescriptions()
        {
            try
            {
                var description = await _context.Describes
                    .Take(4)
                    .OrderByDescending(d => d.Id) // Assuming Id is a unique identifier
                    .Select(d => new { d.DatasetName, d.Column, d.Count, d.Mean, d.Std, d.Min, d.TwentyFivePercent, d.FiftyPercent, d.SeventyFivePercent, d.Max, d.CreatedAT }) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (description.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(description);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        public async Task<IActionResult> ExecuteInfoPyScript(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    return NotFound();
                }

                string datasetPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-Economic-Analysis\Info\Info.py";
                
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
                            sw.WriteLine(@"python.exe " + datasetPath + @" """ + datasetName + @""" """);
                            sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        process.WaitForExit();
                    }
                }

                await Task.Delay(3);

                // var infos = await _context.Infos.OrderByDescending(d => d.Id).Where(d => d.DatasetName == Path.GetFileName(datasetName)).ToListAsync();
                var infos = await _context.Infos
                    .Where(d => d.DatasetName == Path.GetFileName(datasetName))
                    .OrderByDescending(d => d.Id) // Assuming Id is a unique identifier
                    .Select(d => new { d.Information, d.CreatedAT }) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (infos.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(infos);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetColumns()
        {
            try
            {
                var columns = await _context.Columns.Take(4).OrderByDescending(d => d.Id).Select(d => new { d.Column, d.CreatedAT}).Distinct().ToListAsync();
                if (columns.Count == 0)
                {
                    return NoContent();
                }
                return Ok(columns);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetInfos()
        {
            try
            {
                var infos = await _context.Infos.Take(4)
                    .OrderByDescending(d => d.Id) // Assuming Id is a unique identifier
                    .Select(d => new { d.Information, d.CreatedAT }) // Select the desired columns
                    .Distinct() // Ensure distinct results
                    .ToListAsync();
                if (infos.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(infos);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDocumentInfos(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return NotFound();
                }
                var infos = await _context.Infos
                    .Where(d => d.DatasetName == datasetName)
                    .OrderByDescending(d => d.Id) // Assuming Id is a unique identifier
                    .Select(d => new { d.Information, d.CreatedAT }) // Ensure distinct results
                    .ToListAsync();

                if (infos.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(infos);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDocumentColumns(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return NotFound();
                }
                var columns = await _context.Columns.Where(d => d.DatasetName == datasetName).OrderByDescending(d => d.Id).Select(d => new { d.Column, d.CreatedAT}).ToListAsync();
                if (columns.Count == 0)
                {
                    return NoContent();
                }
                return Ok(columns);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDocumentInfoDatasetNames()
        {
            try
            {
                var infos = await _context.Infos.OrderByDescending(d => d.Id).Select(d => d.DatasetName).Distinct().ToListAsync();
                if (infos.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(infos);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Performance()
        {
            return View();
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Documents.Take(500).OrderByDescending(_id => _id.Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentsByYear()
        {
            try
            {
                DateTime firstDocumentCreatedAT = await _context.Documents.OrderBy(d => d.Id).Select(d => d.CreatedAT).FirstOrDefaultAsync();
                var numberOfDocumentsByYear = await _context.Documents
                    .Where(d => d.CreatedAT >= firstDocumentCreatedAT)
                    .GroupBy(d => d.CreatedAT.Year)
                    .Select(g => new {
                        year = g.Key,
                        numberOfDocumentsByYear = g.Count()
                    })
                    .ToListAsync();
                if (numberOfDocumentsByYear.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(numberOfDocumentsByYear);    
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDocumentsByMonth()
        {
            try
            {
                DateTime firstDocumentCreatedAT = await _context.Documents.OrderBy(d => d.Id).Select(d => d.CreatedAT).FirstOrDefaultAsync();
                var numberOfDocumentsByMonth = await _context.Documents
                    .Where(d => d.CreatedAT >= firstDocumentCreatedAT)
                    .GroupBy(d => d.CreatedAT.Month)
                    .Select(g => new {
                        Month = g.Key,
                        numberOfDocumentsByMonth = g.Count()
                    })
                    .ToListAsync();
                if (numberOfDocumentsByMonth.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(numberOfDocumentsByMonth);    
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDocumentsByDay()
        {
            try
            {
                DateTime firstDocumentCreatedAT = await _context.Documents.OrderBy(d => d.Id).Select(d => d.CreatedAT).FirstOrDefaultAsync();
                var numberOfDocumentsByDay = await _context.Documents
                    .Where(d => d.CreatedAT >= firstDocumentCreatedAT)
                    .GroupBy(d => d.CreatedAT.Day)
                    .Select(g => new {
                        Day = g.Key,
                        numberOfDocumentsByDay = g.Count()
                    })
                    .ToListAsync();
                if (numberOfDocumentsByDay.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(numberOfDocumentsByDay);    
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentsByYearAndMonth()
        {
            try
            {
                DateTime firstDocumentCreatedAT = await _context.Documents.OrderBy(d => d.Id).Select(d => d.CreatedAT).FirstOrDefaultAsync();
                var numberOfDocumentsByYearAndMonth = await _context.Documents
                    .Where(d => d.CreatedAT >= firstDocumentCreatedAT)
                    .GroupBy(d => new { d.CreatedAT.Year, d.CreatedAT.Month })
                    .Select(g => new {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        numberOfDocumentsByYearAndMonth = g.Count()
                    })
                    .ToListAsync();
                if (numberOfDocumentsByYearAndMonth.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(numberOfDocumentsByYearAndMonth);    
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentsByMonthAndDay()
        {
            try
            {
                DateTime firstDocumentCreatedAT = await _context.Documents.OrderBy(d => d.Id).Select(d => d.CreatedAT).FirstOrDefaultAsync();
                
                var numberOfDocumentsByMonthAndDay = await _context.Documents
                    .Where(d => d.CreatedAT >= firstDocumentCreatedAT)
                    .GroupBy(d => new { d.CreatedAT.Month, d.CreatedAT.Day })
                    .Select(g => new {
                        month = g.Key.Month,
                        day = g.Key.Day,
                        numberOfDocumentsByMonthAndDay = g.Count()
                    })
                    .ToListAsync();
                if (numberOfDocumentsByMonthAndDay.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(numberOfDocumentsByMonthAndDay);    
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDocumentsByDayAndHour()
        {
            try
            {
                DateTime firstDocumentCreatedAT = await _context.Documents.OrderBy(d => d.Id).Select(d => d.CreatedAT).FirstOrDefaultAsync();
                
                var numberOfDocumentsByDayAndHour = await _context.Documents
                    .Where(d => d.CreatedAT >= firstDocumentCreatedAT)
                    .GroupBy(d => new { d.CreatedAT.Day, d.CreatedAT.Hour })
                    .Select(g => new {
                        day = g.Key.Day,
                        hour = g.Key.Hour,
                        numberOfDocumentsByDayAndHour = g.Count()
                    })
                    .ToListAsync();
                if (numberOfDocumentsByDayAndHour.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(numberOfDocumentsByDayAndHour);    
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return NotFound();
            }
            var documents = await _context.Documents
                .Where(d => d.Title.Contains($"{title}")).ToListAsync();
            
            return View(documents);
        }
        [HttpGet]
        public async Task<IActionResult> SearchById(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound();
            }
            var documents = await _context.Documents
                .Where(d => d.Id == id).ToListAsync();
            
            return View(documents);
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }
        // GET: Documents/Details/5
        public async Task<IActionResult> ColumnDescriptionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.EntityDescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,CreatedAT")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,CreatedAT")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> ColumnDescriptionEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.EntityDescriptions.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ColumnDescriptionEdit(int id, [Bind("Id,ColumnID,Column,Description,CreatedAT")] ColumnDescription columnDescription)
        {
            if (id != columnDescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(columnDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnDescriptionExists(columnDescription.Id))
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
            return View(columnDescription);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Documents/Delete/5
        public async Task<IActionResult> DeleteColumnDescription(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columnDescription = await _context.EntityDescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (columnDescription == null)
            {
                return NotFound();
            }

            return View(columnDescription);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("DeleteColumnDescription")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteColumnDescriptionConfirmed(int id)
        {
            var columnDescription = await _context.EntityDescriptions.FindAsync(id);
            _context.EntityDescriptions.Remove(columnDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
        private bool ColumnDescriptionExists(int id)
        {
            return _context.EntityDescriptions.Any(e => e.Id == id);
        }
    }
}
