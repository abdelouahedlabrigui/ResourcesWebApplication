using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.GenerativeAI;
using ResourcesWebApplication.Models.SourceCodes.AtmosphericSciences;

namespace ResourcesWebApplication.Controllers.SourceCodes
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtmosphericSciencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AtmosphericSciencesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("DeleteAtmosphericCodeLinePrompt")]
        public async Task<IActionResult> DeleteAtmosphericCodeLinePrompt(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }
                var query = await _context.AtmosphericCodeLinePrompts
                    .Where(w => w.Id == id)
                    .ToListAsync();
                if (query.Count > 0)
                {
                    _context.AtmosphericCodeLinePrompts.Remove(query[0]);
                    await _context.SaveChangesAsync();
                    return Ok(new {Message = "Data removed."}); 
                }
                return Ok(new {Message = "No data to remove."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetPromptsByIntervals")]
        public async Task<IActionResult> GetPromptsByIntervals(int interval_1, int interval_2)
        {
            if (string.IsNullOrEmpty(interval_1.ToString()) || string.IsNullOrEmpty(interval_2.ToString()) || interval_1 > interval_2)
            {
                return NotFound();
            }
            var query = await _context.CodeGenInterpretions
                .Where(w => w.Id >= interval_1)
                .Where(w => w.Id <= interval_2)
                .Select(s => new CodeGenInterpretion {
                    Id = s.Id,
                    LlmAgent = s.LlmAgent,
                    CodeLine = s.CodeLine,
                    Interpretation = s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", "").Replace("\n", " ").Replace("|", "").Replace("â", "").Replace("ˆ©", ""),
                })
                .ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        [Route("GetAtmosphericCodeLinePromptsByProblem")]
        public async Task<IActionResult> GetAtmosphericCodeLinePromptsByProblem(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return NotFound();
                }
                var query = await _context.AtmosphericCodeLinePrompts
                    .Where(w => w.Name == name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetAtmosphericCodeLinePrompt")]
        public async Task<IActionResult> GetAtmosphericCodeLinePrompt()
        {
            try
            {
                var query = await _context.AtmosphericCodeLinePrompts
                    .Take(5)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                var prompt = await _context.CodeGenInterpretions
                    .Take(1)                    
                    .OrderByDescending(o => o.Id)
                    .Select(s => new {
                        Interpretation = s.Interpretation.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", "")
                    })
                    .ToListAsync();
                return Ok(new {query = query, prompt = prompt});
            }
            catch (System.Exception ex)
            {                
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("PostAtmosphericCodeLinePrompt")]
        public async Task<IActionResult> PostAtmosphericCodeLinePrompt(
            string code_snippet_name, string code_line, string prompt)
        {
            try
            {
                // if (string.IsNullOrWhiteSpace(code_snippet_name) || string.IsNullOrWhiteSpace(code_line) || string.IsNullOrEmpty(prompt))
                // {
                //     return NotFound();
                // }
                AtmosphericCodeLinePrompt codeLinePrompt = new AtmosphericCodeLinePrompt
                {
                    Name = code_snippet_name,
                    CodeLine = code_line,
                    Prompt = prompt,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = $"Model state: {ModelState.IsValid}"});
                }
                var match = await _context.AtmosphericCodeLinePrompts
                    .Where(w => w.Prompt == prompt)
                    .ToListAsync();
                if (match.Count == 0)
                {
                    _context.AtmosphericCodeLinePrompts.Add(codeLinePrompt);
                    await _context.SaveChangesAsync();
                    return Ok(new {Message = "Data saved, to table."});   
                }
                return Ok(new {Message = "Data already in database."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetCodeSippetNames")]
        public async Task<IActionResult> GetCodeSippetNames()
        {
            try
            {
                var query = await _context.AtmosphericCodeLines
                    .GroupBy(g => g.Name)
                    .Select(s => new {Name = s.Key, Counts = s.Count()})
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetLinesFromCodeSnippet")]
        public async Task<IActionResult> GetLinesFromCodeSnippet(string codeSnippet)
        {
            try
            {
                if (string.IsNullOrEmpty(codeSnippet))
                {
                    return Ok(new {Message = $"Not found: {codeSnippet}"});
                }
                var query = await _context.AtmosphericCodeLines
                    .Where(w => w.Name == codeSnippet)
                    .OrderByDescending(o => o.Id)
                    .Select(s => new {Name = s.Name, CodeLine = s.CodeLine})
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query counts: {query.Count}."});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("PostSourceCodeFilesCodeLines")]
        public async Task<IActionResult> PostSourceCodeFilesCodeLines()
        {
            string source_code_path = 
                @"C:\Users\dell\Entrepreneurship\Engineering\Scripts\Algorithms\AtmosphericStudies\exercises_6_8";
            FileInfo fileInfo = null;
            List<AtmosphericCodeLine> codeLines = new List<AtmosphericCodeLine>();
            string[] files = System.IO.Directory.GetFiles(source_code_path);
            foreach (string file in files)
            {
                fileInfo = new FileInfo(file);
                string[] fileContents = System.IO.File.ReadAllText(@"" + fileInfo.FullName).Split('\n');
                foreach (var line in fileContents)
                {                    
                    if (!string.IsNullOrWhiteSpace(line))
                    {                    
                        AtmosphericCodeLine atmosphericFile = new AtmosphericCodeLine
                        {
                            FullAddress = fileInfo.FullName.ToString(),
                            Name = fileInfo.Name.ToString(),
                            CodeLine = line,
                            CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        };
                        // codeLines.Add(atmosphericFile);
                        _context.AtmosphericCodeLines.Add(atmosphericFile);
                    }
                }
            }
            // _context.AtmosphericCodeLines.AddRange(codeLines);
            await _context.SaveChangesAsync();
            return Ok(new {Message = $"File Code Lines saved: Count Rows: {_context.AtmosphericCodeLines.Count()}"});
        }
        [HttpGet]
        [Route("PostSourceCodeFilesInfo")]
        public async Task<IActionResult> PostSourceCodeFilesInfo()
        {
            string source_code_path = 
                @"C:\Users\dell\Entrepreneurship\Engineering\Scripts\Algorithms\AtmosphericStudies\exercises_6_8";
            FileInfo fileInfo = null;
            List<AtmosphericFileInfo> fileInfos = new List<AtmosphericFileInfo>();
            string[] files = System.IO.Directory.GetFiles(source_code_path);
            foreach (string file in files)
            {
                fileInfo = new FileInfo(file);
                AtmosphericFileInfo atmosphericFile = new AtmosphericFileInfo
                {
                    FullAddress = fileInfo.FullName.ToString(),
                    Name = fileInfo.Name.ToString(),
                    LastWriteTime = fileInfo.LastWriteTime.ToString(),
                    LastAccessTime = fileInfo.LastAccessTime.ToString(),
                    DirectoryName = fileInfo.DirectoryName.ToString(),
                    Length = fileInfo.Length.ToString(),
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                fileInfos.Add(atmosphericFile);
            }
            _context.AtmosphericFileInfos.AddRange(fileInfos);
            await _context.SaveChangesAsync();
            return Ok(new {Message = $"File Info saved: Count Rows: {_context.AtmosphericFileInfos.Count()}"});
        } 
    }
}