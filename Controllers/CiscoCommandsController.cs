using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Cisco;
using ResourcesWebApplication.Models.Commands;
using ResourcesWebApplication.Models.Context;


namespace ResourcesWebApplication.Controllers
{
    public class CiscoCommandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CiscoCommandsController(ApplicationDbContext context)
        {
            _context = context;
            // _telnetClient = new telne
        }

        private static string SendTelnetCommand(string ip, int port, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(ip, port))
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) {AutoFlush = true})
                {
                    string response = reader.ReadLine();
                    writer.WriteLine(command);
                    StringBuilder output = new StringBuilder();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        output.AppendLine(line);                        
                    }
                    return output.ToString();
                }
            }
            catch (System.Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        [HttpGet]
        public IActionResult GetCommandOutput()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> PostNetworkAutomationConnection(string protocol, string command)
        {
            try
            {
                Connect connect = new Connect
                {
                    Protocol = protocol,
                    Command = command,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                _context.NetworkAutomationConnections.Add(connect);
                await _context.SaveChangesAsync();
                
                return Ok(new { Message = $"Connection checked at: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}" });
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }

        // GET: CiscoCommands
        public async Task<IActionResult> Index()
        {
            return View(await _context.CiscoCommands.Take(5).OrderByDescending(d => d.Id).ToListAsync());
        }

        // GET: CiscoCommands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciscoCommand = await _context.CiscoCommands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciscoCommand == null)
            {
                return NotFound();
            }

            return View(ciscoCommand);
        }

        // GET: CiscoCommands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CiscoCommands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Command")] CiscoCommand ciscoCommand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciscoCommand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciscoCommand);
        }

        [HttpPost]
        public async Task<IActionResult> PostCiscoCommands([FromBody] List<CiscoCommand> ciscoCommand)
        {
            try
            {
                foreach (var item in ciscoCommand)
                {
                    CiscoCommand command = new CiscoCommand()
                    {
                        Title = item.Title,
                        Description = item.Description,
                        Command = item.Command,
                    };
                    _context.Add(command);
                    await _context.SaveChangesAsync();
                }
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // GET: CiscoCommands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciscoCommand = await _context.CiscoCommands.FindAsync(id);
            if (ciscoCommand == null)
            {
                return NotFound();
            }
            return View(ciscoCommand);
        }

        // POST: CiscoCommands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Command")] CiscoCommand ciscoCommand)
        {
            if (id != ciscoCommand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciscoCommand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiscoCommandExists(ciscoCommand.Id))
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
            return View(ciscoCommand);
        }

        // GET: CiscoCommands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciscoCommand = await _context.CiscoCommands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciscoCommand == null)
            {
                return NotFound();
            }

            return View(ciscoCommand);
        }

        // POST: CiscoCommands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciscoCommand = await _context.CiscoCommands.FindAsync(id);
            _context.CiscoCommands.Remove(ciscoCommand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiscoCommandExists(int id)
        {
            return _context.CiscoCommands.Any(e => e.Id == id);
        }
    }
}
