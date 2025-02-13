using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.GenerativeAI;
using ResourcesWebApplication.Models.Prompts;
using ResourcesWebApplication.Models.Prompts.MachineLearning;

namespace ResourcesWebApplication.Controllers
{
    public class PromptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromptsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetTerraformPrompts()
        {
            try
            {
                var query = await _context.TerraformPromptsRequests
                    .Select(s => new {Text = s.Text, Prompt = s.Prompt.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace(@"\", "").Replace("\n", "")})                    
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count: {query.Count}"});                    
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTopTerraformPrompts()
        {
            try
            {
                var query = await _context.TerraformPromptsRequests
                    .Take(1)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count: {query.Count}"});                    
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostDatasetColumnsInformationInterpretations([FromBody] GenInfoInterpretation gen)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Model isn't valid."});
                }
                var query = await _context.GenInfoInterpretations
                    .Where(w => w.Information == gen.Information)
                    .ToListAsync();
                if (query.Count == 0)
                {                    
                    _context.GenInfoInterpretations.Add(gen);
                    await _context.SaveChangesAsync();
                    return Ok(new {Message = $"Posted prompt."});
                } else {
                    return Ok(new {Message = "Information already in table."});
                }
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostPromptsFromSearches([FromBody] TerraformPrompts prompts)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Model isn't valid."});
                }
                var query = await _context.TerraformPromptsRequests
                    .Where(w => w.Text == prompts.Text)
                    .ToListAsync();
                if (query.Count == 0)
                {                    
                    _context.TerraformPromptsRequests.Add(prompts);
                    await _context.SaveChangesAsync();
                    return Ok(new {Message = $"Posted prompt."});
                } else {
                    return Ok(new {Message = "Text already in table."});
                }
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSentencesBySearch(string root_head, string verb, string propn)
        {
            try
            {
                if (string.IsNullOrEmpty(root_head) && string.IsNullOrEmpty(verb) && string.IsNullOrEmpty(propn))
                {
                    return Ok(new {Message = "datasetName, root_head, verb & propn parameters are required."});
                }
                var query = await _context                    
                    .NLPSentences      
                    .Where(w => w.Sentence.Contains($"{propn}"))
                    .Where(w => w.Sentence.Contains($"{root_head}"))
                    .Where(w => w.Sentence.Contains($"{verb}"))
                    .Where(w => w.DatasetName == "ch-1.txt")
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query counts: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPartsOfSpeechPROPNCounts()
        {
            try
            {
                var query = await _context                    
                    .PartOfSpeech   
                    .Where(w => w.DatasetName == "ch-1.txt")
                    .Where(w => w.Pos_ == "PROPN")   
                    .GroupBy(g => g.Lemma_)
                    .Select(s => new {Lemma_ = s.Key, Counts = s.Count().ToString()})
                    .OrderByDescending(o => o.Counts)
                    .ToListAsync();
                    
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query counts: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPartsOfSpeechVerbsCounts()
        {
            try
            {
                var query = await _context                    
                    .PartOfSpeech      
                    .Where(w => w.DatasetName == "ch-1.txt")
                    .Where(w => w.Pos_ == "VERB")                       
                    .GroupBy(g => g.Text_)
                    .Select(s => new {Lemma_ = s.Key, Counts = s.Count().ToString()})
                    .OrderByDescending(o => o.Counts) 
                    .ToListAsync();                    
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query counts: {query.Count}" });
                }
                var counts = query
                    .Where(w => int.Parse(w.Counts) > 10)
                    .OrderByDescending(o => o.Counts)
                    .ToList();
                return Ok(counts);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNounChunksByRootTextFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return Ok(new {Message = "Filter is required."});
            }
            var query = await _context.NounChunks
                // .FromSqlRaw($"SELECT * FROM NounChunks WHERE Text LIKE '%{filter}%'")
                .Where(w => w.DatasetName == "ch-1.txt")
                .Where(w => w.Text.Contains($"{filter}"))
                .GroupBy(g => g.RootText)
                .Select(s => new { RootText = s.Key, Counts = s.Count().ToString()})
                .ToListAsync();
            var counts = query
                .Where(w => int.Parse(w.Counts) > 1)
                .OrderByDescending(o => o.Counts)
                .ToList();
                
            if (query.Count == 0)
            {
                return Ok(new {Message = $"Query counts: {query.Count}" });
            }
            return Ok(counts);
        }
        [HttpGet]
        public async Task<IActionResult> GetNounChunksRootHeadCounts()
        {
            try
            {
                var query = await _context.NounChunks
                    .Where(w => w.DatasetName == "ch-1.txt")
                    .GroupBy(g => g.RootText)
                    .Select(s => new {RootHead = s.Key, Counts = s.Count().ToString() })
                    .OrderByDescending(o => o.Counts)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query counts: {query.Count}" });
                }
                var counts = query
                    .Where(w => int.Parse(w.Counts) > 10)
                    .OrderByDescending(o => o.Counts)
                    .ToList();
                return Ok(counts);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> FilterPrompt(string filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter))
                {
                    return Ok(new {Message = $"Filter Length: {filter.Length}" });
                }
                var query = await _context.Prompts
                    .Where(w => w.Title.Contains($"{filter}"))
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPrompts()
        {
            try
            {
                var query = await _context.Prompts
                    .Where(w => w.Id >= 30002)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCodeFileVersionInfoFileName()
        {
            try
            {
                var query = await _context.VersionInfos
                    .Where(w => w.FileName != "None")
                    .OrderByDescending(o => o.Id)
                    .Select(s => new { s.Id, s.FileName, Root = new FileInfo(s.FileName).Name})
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
        [HttpGet]
        public async Task<IActionResult> GetCodeInterpretationCommentLines()
        {
            try
            {
                var query = await _context.CodeGenInterpretions
                    .Where(w => w.CodeLine.StartsWith("# "))
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();  
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count {query.Count}" });
                } else {
                    return Ok(query);
                }                
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCodeInterpretationCommentLinesByIdsInterval(int id1, int id2)
        {
            try
            {
                if (id1 > id2)
                {
                    return Ok(new {Message = "Error of ids"});
                }
                var query = await _context.CodeGenInterpretions
                    .Where(w => w.Id >= id1)
                    .Where(w => w.Id <= id2)
                    .Select(s => new CodeGenInterpretion {
                        Id = s.Id,
                        LlmAgent = s.LlmAgent,
                        CodeLine = s.CodeLine,
                        Interpretation = s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", " "),
                        CreatedAT = s.CreatedAT
                    })
                    .ToListAsync();  
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count {query.Count}" });
                } else {
                    return Ok(query);
                }                
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCodeInterpretations()
        {
            try
            {
                var query = await _context.CodeGenInterpretions
                    .Select(s => new CodeGenInterpretion {
                        Id = s.Id,
                        LlmAgent = s.LlmAgent,
                        CodeLine = s.CodeLine,
                        Interpretation = s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", "").Replace(@"\n", " "),
                        CreatedAT = s.CreatedAT
                    })
                    .ToListAsync();  
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count {query.Count}" });
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCodeInterpretationIfInterpretationsHavePromptError()
        {
            try
            {
                var query = await _context.CodeGenInterpretions
                    .OrderByDescending(o => o.Id)
                    .Select(s => s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", "").Replace(@"\", "").Replace(@"\n", " "))
                    .ToListAsync();  
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query count {query.Count}" });
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }
        // [HttpGet]
        // public async Task<IActionResult> UpdateCodeGeneratedInterpretationRemoveCommentLines()
        // {
        //     try
        //     {
        //         var query = await _context.CodeGenInterpretions
        //             .Where(w => w.CodeLine.StartsWith("# "))
        //             .OrderByDescending(o => o.Id)
        //             .ToListAsync();                
        //         _context.CodeGenInterpretions.RemoveRange(query);
        //         await _context.SaveChangesAsync();

        //         var deleted = await _context.CodeGenInterpretions   
        //             .Where(w => w.CodeLine.StartsWith("# "))
        //             .OrderByDescending(o => o.Id)
        //             .ToListAsync();
        //         if (deleted.Count > 0)
        //         {
        //             return Ok(new { Message = "Deleted Query returned with no data deleted." });
        //         } else {
        //             return Ok(new { Message = "Data deleted successfully." });
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return Ok(new { Message = ex.Message });
        //     }
        // }
        [HttpGet]
        public async Task<IActionResult> UpdateCodeGeneratedInterpretationCleanCodeInterpretationErrorMessages()
        {
            try
            {
                var query = await _context.CodeGenInterpretions
                    .Select(s => new CodeGenInterpretion {
                        Id = s.Id,
                        LlmAgent = s.LlmAgent,                        
                        CodeLine = s.CodeLine,
                        Interpretation = s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", "").Replace(@"\", "").Replace(@"\n", " "),
                        CreatedAT = s.CreatedAT,
                    })
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                _context.CodeGenInterpretions.UpdateRange(query);
                await _context.SaveChangesAsync();
                var updated = await _context.CodeGenInterpretions
                    .Where(w => w.Interpretation.Contains("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n "))
                    .ToListAsync();
                if (updated.Count > 0)
                {
                    return Ok(new { Message = "Updated Query returned with no data updated." });
                } else {
                    return Ok(new { Message = "Data updated successfully." });
                }
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostCodeGeneratedInterpretation([FromBody] CodeGenInterpretion codeGenInterpretion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.CodeGenInterpretions.Add(codeGenInterpretion);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Code generated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostMLConcepts([FromBody] MLConcept concept)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.MLConcepts.Add(concept);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Definition generated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetMLConcepts()
        {
            try
            {
                var query = await _context.MLConcepts
                    .Take(10)
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
        [HttpGet]
        public async Task<IActionResult> GetMLDefinitionByConcept(string concept)
        {
            try
            {
                var query = await _context.MLConcepts
                    .Where(w => w.Concept == concept)
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
        [HttpGet]
        public async Task<IActionResult> GetCommandGenInterpretation(string os)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(os))
                {
                    return Ok(new {Message = "OS is required, to filter table."});
                }
                var query = await _context.CommandGenInterpretations
                    .Select(s => new {  
                        OperationSystem = s.OperationSystem, 
                        Technology = s.Technology, 
                        Command = s.Command, 
                        Interpretation = s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", "").Replace("\n", "") })
                    .Where(c => c.Technology == "Terraform")
                    .Where(w => w.OperationSystem == os)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count is {query.Count}." });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCommandsInterpretationsByFilter(string filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter))
                {
                    return Ok(new {Message = $"Filter is {filter}." });  
                }
                var query = await _context.CommandGenInterpretations
                    .Where(c => c.OperationSystem.Contains($"{filter}"))
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count is {query.Count}." });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCommandsByFilter(string filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter))
                {
                    return Ok(new {Message = $"Filter is {filter}." });  
                }
                var query = await _context.Commands
                    .Where(c => c.Title.Contains($"{filter}"))
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count is {query.Count}." });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCommandsInterpretations()
        {
            try
            {
                var query = await _context.CommandGenInterpretations
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count is {query.Count}." });
                }
                _context.CommandGenInterpretations.RemoveRange(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "All interpretation deleted."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCommandsInterpretationById(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return Ok(new {Message = $"Id is {id}"});
                }
                var query = await _context.CommandGenInterpretations
                    .Where(w => w.Id == id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query count is {query.Count}." });
                }
                _context.CommandGenInterpretations.RemoveRange(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Interpretation with {id} deleted."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostCommandGenInterpretation([FromBody] CommandGenInterpretation commandGenInterpretation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.CommandGenInterpretations.Add(commandGenInterpretation);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Code generated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostCodeGen([FromBody] CodeGen codeGen)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.CodeGenerators.Add(codeGen);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Code generated successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCodeGenerators()
        {
            try
            {
                var query = await _context.CodeGenerators
                    .Take(1)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneratedCode()
        {
            try
            {
                var query = await _context.CodeGenerators
                    .Take(1)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                string[] fileContent = System.IO.File.ReadAllText(query[0].FileName).Replace("\n\n", "\n").Split('\n');
                List<object> lines = new List<object>();
                foreach (string line in fileContent)
                {
                    if(!line.Contains("failed"))
                    {
                        var dataLine = new {
                            Line = line,
                        };
                        lines.Add(dataLine);
                    }
                }
                return Ok(lines);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = $"Error loading file: {ex.Message}" });
            }
        }
        

        // GET: Prompts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prompts.OrderByDescending(b => b.Id).ToListAsync());
        }
        

        // GET: Prompts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prompt == null)
            {
                return NotFound();
            }

            return View(prompt);
        }

        // GET: Prompts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prompts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Message,CreatedAT")] Prompt prompt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prompt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prompt);
        }

        // GET: Prompts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompts.FindAsync(id);
            if (prompt == null)
            {
                return NotFound();
            }
            return View(prompt);
        }

        // POST: Prompts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Message,CreatedAT")] Prompt prompt)
        {
            if (id != prompt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prompt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromptExists(prompt.Id))
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
            return View(prompt);
        }

        // GET: Prompts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prompt == null)
            {
                return NotFound();
            }

            return View(prompt);
        }

        // POST: Prompts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prompt = await _context.Prompts.FindAsync(id);
            _context.Prompts.Remove(prompt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromptExists(int id)
        {
            return _context.Prompts.Any(e => e.Id == id);
        }
    }
}
