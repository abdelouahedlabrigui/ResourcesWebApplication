using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Commands;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers
{
    public class CommandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult LaravelENV()
        {
            return View();
        }
        public async Task<IActionResult> LaravelENVContents(string clause)
        {
            var content = await _context.CiscoCommands.Where(d => d.Id >= 3002 && d.Id <= 3038).Where(d => d.Title.Contains($"{clause}")).Select(c => new {c.Description, c.Command}).ToListAsync();
            return Ok(content);
        }
        public async Task<IActionResult> LaravelENVTitles()
        {
            var titles = await _context.CiscoCommands.Where(d => d.Id >= 3002 && d.Id <= 3038).Select(c => new {c.Title}).Distinct().ToListAsync();
            return Ok(titles);
        }
        // GET: Commands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commands.OrderByDescending(d => d.Id).Take(10).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> SearchById(int Id)
        {
            if (string.IsNullOrWhiteSpace(Id.ToString()))
            {
                return NotFound();
            }
            var commands = await _context.Commands
                .Where(d => d.Id == Id).ToListAsync();
            
            return View(commands);
        }
        [HttpGet]
        public async Task<IActionResult> SearchByCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command.ToString()))
            {
                return NotFound();
            }
            var commands = await _context.Commands
                .Where(d => d.Command.Contains($"{command}")).ToListAsync();
            
            return View(commands);
        }
        // GET: Commands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cli = await _context.Commands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cli == null)
            {
                return NotFound();
            }

            return View(cli);
        }

        // GET: Commands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Command,CreatedAT")] Cli cli)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cli);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cli);
        }

        // GET: Commands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cli = await _context.Commands.FindAsync(id);
            if (cli == null)
            {
                return NotFound();
            }
            return View(cli);
        }

        // POST: Commands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Command,CreatedAT")] Cli cli)
        {
            if (id != cli.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CliExists(cli.Id))
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
            return View(cli);
        }

        // GET: Commands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cli = await _context.Commands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cli == null)
            {
                return NotFound();
            }

            return View(cli);
        }

        // POST: Commands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cli = await _context.Commands.FindAsync(id);
            _context.Commands.Remove(cli);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CliExists(int id)
        {
            return _context.Commands.Any(e => e.Id == id);
        }
    }
}
