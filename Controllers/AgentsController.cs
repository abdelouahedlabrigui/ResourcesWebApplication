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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ResourcesWebApplication.Library.Pings;
using ResourcesWebApplication.Library.WazuhServer;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Devops;

namespace ResourcesWebApplication.Controllers
{
    public class AgentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentsController(ApplicationDbContext context)
        {
            _context = context;

        }
        
        [HttpGet]
        public async Task<IActionResult> GetServiceChecksIds()
        {
            var query = await _context.ServiceChecks.Select(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        public async Task<IActionResult> GetServiceChecks()
        {
            var query = await _context.ServiceChecks.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceCheckById(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound();
            }

            var query = await _context.ServiceChecks.Where(d => d.Id == id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthsIds()
        {
            var query = await _context.Auths.Select(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetAuths()
        {
            var query = await _context.Auths.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthById(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound();
            }

            var query = await _context.Auths.Where(d => d.Id == id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }

        [HttpGet]
        public async Task<IActionResult> GetPingResultsIds()
        {
            var query = await _context.PingResults.Select(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetPingResults()
        {
            var query = await _context.PingResults.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetPingResultById(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound();
            }

            var query = await _context.PingResults.Where(d => d.Id == id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }

        [HttpGet]
        public async Task<IActionResult> GetAgentsIds()
        {
            var query = await _context.Agents.Select(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            var query = await _context.Agents.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgentsTop10Desc()
        {
            var query = await _context.Agents.Take(10).OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetLinuxAgentsTop10Desc()
        {
            var query = await _context.LinuxAgents.Take(10).OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgentById(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound();
            }

            var query = await _context.Agents.Where(d => d.Id == id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceStatusIds()
        {
            var query = await _context.ServiceStatus.Select(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceStatus()
        {
            var query = await _context.ServiceStatus.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceStatusById(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound();
            }

            var query = await _context.ServiceStatus.Where(d => d.Id == id).ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        // [HttpGet]
        [HttpPost]
        public async Task<IActionResult> PostWazuhAgents([FromBody] Agent agent)
        {
            try
            {
                if (agent == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Agents.Add(agent);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostWazuhAgentsForLinux([FromBody] LinuxAgent agent)
        {
            try
            {
                if (agent == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.LinuxAgents.Add(agent);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostWazuhServerAuth([FromBody] Auth auth)
        {
            try
            {
                if (auth == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Auths.Add(auth);
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
        public async Task<IActionResult> PostWazuhAPIJwtToken(string auth_ip)
        {
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Cybersecurity\ResourcesWebAPP\PostWazuhAPIJwtToken.py";
                    

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
                        sw.WriteLine($@"python.exe {scriptPath} ""{auth_ip}""");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
            }
            var auths = await _context.Auths.Take(1).OrderByDescending(d => d.Id).ToListAsync();
            return Ok(auths);
        }    
        private List<WazuhAgent> GetAgentsIPs()
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
        [HttpGet]
        public IActionResult GetWazuhAgentsIPs()
        {
            var agents = GetAgentsIPs();
            return Ok(agents);
        }
        [HttpGet]        
        public IActionResult GetWazuhAgentsWithoutDb()
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

                    return Ok(agents);
                }
            }
            return Ok();
        }
        [HttpGet]        
        public IActionResult ExecutePostWazuhAgents()
        {                        
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Cybersecurity\ResourcesWebAPP\ActiveAgents.py";
                    

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

                }
            }
            return Ok(new {Message = "Agents added to ActiveAgents entity."});
        }
        [HttpGet]
        public IActionResult GetPingOfAgentFromVM()
        {
            PingResultMethods pingResultMethods = new PingResultMethods();
            var ping = pingResultMethods.GetPingOfAgentFromVM();
            return Ok(ping);
        }
        [HttpGet]
        public IActionResult GetPingResultFromLinuxDistro(string vmIP, string agent_ip, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(vmIP) || string.IsNullOrWhiteSpace(agent_ip) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return NotFound();
            }
            PingResultMethods pingResultMethods = new PingResultMethods();
            PingResult pingResult = pingResultMethods.GetPingResult(vmIP,agent_ip,username,password);
            return Ok(pingResult);
        }

        [HttpGet]
        public IActionResult CheckServiceStatusForLinuxDistro(string ip, string service, string createdAT)
        {
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(service) || string.IsNullOrWhiteSpace(createdAT))
            {
                return NotFound();
            }
            PingResultMethods pingResultMethods = new PingResultMethods();
            ServiceStatus serviceStatus = pingResultMethods.CheckWazuhServerServicesStatus(ip, service, createdAT);
            return Ok(serviceStatus);
        }
        [HttpGet]                        
        public IActionResult GetStatusRestartWazuhServerService(string ip, string service, string createdAT)
        {
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(service) || string.IsNullOrWhiteSpace(createdAT))
            {
                return NotFound();
            }
            WazuhVMServices wazuhVMServices = new WazuhVMServices();
            ServiceStatus serviceStatus = wazuhVMServices.Restart_WazuhServerService(ip,service,createdAT);
            return Ok(serviceStatus);
        }

        [HttpPost]   
        public async Task<IActionResult> VerifyWazuhServerServiceStatus(int authId, int agent_id, int vmId, string service)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authId.ToString()) || string.IsNullOrWhiteSpace(vmId.ToString()) || string.IsNullOrWhiteSpace(service) || string.IsNullOrWhiteSpace(agent_id.ToString()))
                {
                    return NotFound();
                }
                PingResultMethods pingResultMethods = new PingResultMethods();
                var wazuhServer = await _context.Auths.Where(d => d.Id == authId).ToListAsync();
                var agent = await _context.Agents.Where(d => d.Id == agent_id).ToListAsync();
                var vm = await _context.Auths.Where(d => d.Id == vmId).ToListAsync();
                if (wazuhServer[0].IPAddress == vm[0].IPAddress)
                {
                    return BadRequest("authId has to be a wazuh server ip. vmId has to be only a linux vm.");
                }
                List<Auth> appendAuths = new List<Auth>
                {
                    wazuhServer[0],
                    vm[0]
                };
                string createdAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                // ping to these ips
                IPsToPing ipsToPing = new IPsToPing() {
                    WazuhServerIP = wazuhServer[0].IPAddress,
                    AgentIP = agent[0].Ip,
                    VmIP = vm[0].IPAddress,
                    CreatedAT = createdAT
                };
                List<PingResult> pingResults = new List<PingResult>
                {
                    pingResultMethods.GetPingResult(ipsToPing.VmIP, ipsToPing.VmIP, vm[0].Username, vm[0].Password),
                    pingResultMethods.GetPingResult(ipsToPing.VmIP, ipsToPing.AgentIP, vm[0].Username, vm[0].Password),
                    pingResultMethods.GetPingResult(ipsToPing.VmIP, ipsToPing.WazuhServerIP, vm[0].Username, vm[0].Password)
                };
                ServiceCheck serviceCheck = new ServiceCheck()
                {
                    Agent_ip = agent[0].Ip,
                    Server_ip = wazuhServer[0].IPAddress,
                    VmIP = vm[0].IPAddress,
                    Service = service,
                    VmUser = vm[0].Username,
                    VmPassword = vm[0].Password,
                    CreatedAT = createdAT
                };

                ServiceStatus serviceStatus = pingResultMethods.CheckWazuhServerServicesStatus(wazuhServer[0].IPAddress, service, createdAT);    

                _context.ServiceStatus.Add(serviceStatus);
                await _context.SaveChangesAsync();
                
                var services = await _context.ServiceStatus.Where(d => d.IPAddress == wazuhServer[0].IPAddress && d.CreatedAT == createdAT).OrderByDescending(d => d.Id).ToListAsync();
                VerifyAgent data = new VerifyAgent()
                {
                    Agent = agent[0],
                    PingResults = pingResults,
                    Auth = appendAuths,
                    ServiceStatus = services,
                    ServiceCheck = serviceCheck,
                    IPsToPing = ipsToPing
                };
                return Ok(data);           
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }            
        }
        [HttpGet]
        public async Task<IActionResult> GetWazuhAgentsIds()
        {
            var agents = await _context.Agents.Select(d => d.AgentId).ToListAsync();
            return Ok(agents);
        }
        [HttpGet]
        public async Task<IActionResult> GetWazuhAgentById(string agent_id)
        {
            if (string.IsNullOrWhiteSpace(agent_id))
            {
                return NotFound();
            }
            var agents = await _context.Agents.Where(d => d.AgentId == agent_id).ToListAsync();
            return Ok(agents);
        }
        [HttpGet]
        public async Task<IActionResult> GetWazuhAgents()
        {
            var agents = await _context.Agents.OrderByDescending(d => d.Id).ToListAsync();
            return Ok(agents);
        }

        // GET: Agents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agents.OrderByDescending(d => d.Id).ToListAsync());
        }

        public async Task<IActionResult> LinuxAgents()
        {
            return View(await _context.LinuxAgents.OrderByDescending(d => d.Id).ToListAsync());
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> DetailsLinuxAgent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.LinuxAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Build,Major,Minor,OSName,Platform,Uname,OS_Version,DateAdd,Manager,RegisterIP,AgentId,Version,Group,Status,MergedSum,Ip,Node_name,LastKeepAlive,Group_config_status,ConfigSum,Name,Status_code")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Build,Major,Minor,OSName,Platform,Uname,OS_Version,DateAdd,Manager,RegisterIP,AgentId,Version,Group,Status,MergedSum,Ip,Node_name,LastKeepAlive,Group_config_status,ConfigSum,Name,Status_code")] Agent agent)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.Id))
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
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.Id == id);
        }

        // GET: Agents/Delete/5
        [HttpGet]
        public async Task<IActionResult> DeleteLinuxAgent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.LinuxAgents.FindAsync(id);
            _context.LinuxAgents.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(LinuxAgents));
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("DeleteLinuxAgent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLinuxAgentConfirmed(int id)
        {
            var agent = await _context.LinuxAgents.FindAsync(id);
            _context.LinuxAgents.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
