using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Devops;

namespace ResourcesWebApplication.Controllers
{
    public class ActiveConfigurationsController : Controller
    {        
        private readonly ApplicationDbContext _context;
        public ActiveConfigurationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private List<WazuhAgent> GetAgents()
        {
            string csvFilePath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\data\Cybersecurity\Wazuh\active-agents\active-agents.csv";
            
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Cybersecurity\ResourcesWebAPP\ActiveAgentsWithoutDB.py";                    
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
                        sw.WriteLine($@"python.exe {scriptPath}");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                    

                    List<WazuhAgent> agents;

                    using (var reader = new StreamReader(csvFilePath))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                    }))
                    {
                        agents = csv.GetRecords<WazuhAgent>().ToList();
                    }

                    return agents;
                }
            }
            return new List<WazuhAgent>{};
        }
        private async Task<string> GetActiveConfiguration(string agent_id, string component, string configuration)
        {
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Cybersecurity\ResourcesWebAPP\ActiveConfiguration.py";                    
            string jsonFile = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\data\Cybersecurity\Wazuh\active-configuration\active-config.json";

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
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        // RedirectStandardOutput = true,
                        // RedirectStandardError = true
                    };

                    process.StartInfo = startInfo;
                    process.Start();                                        
                    using (StreamWriter sw = process.StandardInput)
                    {
                        if (sw.BaseStream.CanWrite)
                        {
                            sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                            sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                            sw.WriteLine($@"python.exe {scriptPath} ""{agent_id}"" ""{component}"" ""{configuration}""");
                            sw.WriteLine(@"deactivate");
                        }
                    }
                    process.WaitForExit();
                }
            }

            await Task.Delay(10);

            // Check if the JSON file exists before reading
            if (!System.IO.File.Exists(jsonFile))
            {
                throw new FileNotFoundException($"JSON file not found: {jsonFile}");
            }

            // Read and parse the JSON content
            string jsonContent = await System.IO.File.ReadAllTextAsync(jsonFile);

            // Log the JSON content for debugging
            Console.WriteLine("JSON Content:");
            Console.WriteLine(jsonContent);

            // Parse the JSON content
            JObject jsonParsed;
            try
            {
                jsonParsed = JObject.Parse(jsonContent);
            }
            catch (JsonReaderException ex)
            {
                throw new InvalidDataException("The JSON content is not valid.", ex);
            }

            string prettyJson = jsonParsed.ToString(Newtonsoft.Json.Formatting.Indented);
            return prettyJson;
        }
  
        [HttpGet]
        public async Task<IActionResult> GetJsonStringForActiveConfiguration(string agent_id, string component, string configuration)
        {
            if (string.IsNullOrWhiteSpace(agent_id) || string.IsNullOrWhiteSpace(component) || string.IsNullOrWhiteSpace(configuration))
            {
                return NotFound();
            }
            string active_config = await GetActiveConfiguration(agent_id,component,configuration);
            return Ok(active_config);
        }
        [HttpGet]
        public IActionResult GetWazuhAgents()
        {
            var agents = GetAgents();
            return Ok(agents);
        }
        [HttpGet]
        public async Task<IActionResult> GetDataFromActiveConfigurationsEntity()
        {
            var query = await _context.ActiveConfigurations.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetDataFromActiveConfigurationsEntityByComponent(string component)
        {
            if (string.IsNullOrWhiteSpace(component))
            {
                return NotFound();
            }
            var query = await _context.ActiveConfigurations.Where(d => d.Component == component).OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }                
        [HttpGet]        
        public async Task<IActionResult> GetFromActiveConfigurationsEntityGroupedComponents()
        {
            var query = await _context.ActiveConfigurations
                .GroupBy(d => d.Component)
                .Select(d => new { Component = d.Key, Count = d.Count() })
                .ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public IActionResult GetAgentsIds()
        {
            var agents = GetAgents().Select(d => d.AgentId).ToList();
            if (agents.Count == 0)
            {
                return NoContent();
            }
            return Ok(agents);
        }                
        [HttpGet]
        public IActionResult GetAgentById(string agent_id)
        {
            if (string.IsNullOrWhiteSpace(agent_id))
            {
                return NotFound();
            }
            var agent = GetAgents()
                .Select(d => new {d.AgentId, d.OSName, d.Platform, d.Ip, d.Name})
                .Where(d => d.AgentId == agent_id).ToList();
            if (agent.Count == 0)
            {
                return NoContent();
            }
            return Ok(agent);            
        }                   
        private async Task<string> AccessActiveConfigurationPlotEncoding()
        {
            string encoding_string = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\data\Cybersecurity\Wazuh\plots\active_configuration_plot_encoding.txt";
            string encoding = await System.IO.File.ReadAllTextAsync(encoding_string);            
            return encoding;
        }        
        [HttpGet]
        public async Task<IActionResult> GetActiveConfigurationPlotEncoding()
        {
            string encoding_string = await AccessActiveConfigurationPlotEncoding();
            if (encoding_string == null)
            {
                return NoContent();
            }
            return Ok(new {Encoding = encoding_string});
        }
        [HttpPost]
        public async Task<IActionResult> PostActiveConfigurations([FromBody] ActiveConfiguration activeConfiguration)
        {
            try
            {
                if (activeConfiguration == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.ActiveConfigurations.Add(activeConfiguration);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
        public IActionResult QueryActiveConfig()
        {
            return View();
        }

        // GET: ActiveConfigurations
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActiveConfigurations.ToListAsync());
        }

        // GET: ActiveConfigurations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeConfiguration = await _context.ActiveConfigurations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeConfiguration == null)
            {
                return NotFound();
            }

            return View(activeConfiguration);
        }

        // GET: ActiveConfigurations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActiveConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Component,Configuration,Tag")] ActiveConfiguration activeConfiguration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activeConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activeConfiguration);
        }

        // GET: ActiveConfigurations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeConfiguration = await _context.ActiveConfigurations.FindAsync(id);
            if (activeConfiguration == null)
            {
                return NotFound();
            }
            return View(activeConfiguration);
        }

        // POST: ActiveConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Component,Configuration,Tag")] ActiveConfiguration activeConfiguration)
        {
            if (id != activeConfiguration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activeConfiguration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiveConfigurationExists(activeConfiguration.Id))
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
            return View(activeConfiguration);
        }

        // GET: ActiveConfigurations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activeConfiguration = await _context.ActiveConfigurations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activeConfiguration == null)
            {
                return NotFound();
            }

            return View(activeConfiguration);
        }

        // POST: ActiveConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activeConfiguration = await _context.ActiveConfigurations.FindAsync(id);
            _context.ActiveConfigurations.Remove(activeConfiguration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiveConfigurationExists(int id)
        {
            return _context.ActiveConfigurations.Any(e => e.Id == id);
        }
    }
}
