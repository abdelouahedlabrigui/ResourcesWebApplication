using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Games.TwoPlayers;

namespace ResourcesWebApplication.Controllers
{
    public class TwoPlayersStrategiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TwoPlayersStrategiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult TwoPlayersStrategies()
        {
            try
            {
                int[,] matrix = new int[3,3] {{3,2,1}, {2,1,3}, {1,3,2}};
                int[] fthStrategies = {2,4,6};
                int[] sndStrategies = {1,3,5};
                Dictionary<char, int> fthPreferences = new Dictionary<char, int>() {{'M', 3}, {'I', 2}, {'J', 1}};
                Dictionary<char, int> sndPreferences = new Dictionary<char, int>() {{'I', 3}, {'M', 2}, {'J', 1}};
                List<object> results = new List<object>();
                foreach (var fthStrategy in fthStrategies)
                {
                    foreach (var sndStrategy in sndStrategies)
                    {
                        int sum = fthStrategy + sndStrategy;
                        char fthPreference = sum <= 5 ? 'M' : sum == 7 ? 'I' : 'J';
                        char sndPreference = sum == 7 ? 'I' : sum <= 5 ? 'M' : 'J';
                        int fthUtility = fthPreferences[fthPreference];
                        int sndUtility = sndPreferences[sndPreference];
                        var result = new 
                        {
                            FthStrategy = fthStrategy.ToString(),
                            SndStrategy = sndStrategy.ToString(),
                            Sum = sum.ToString(),
                            FthPreference = fthPreference.ToString(),
                            SndPreference = sndPreference.ToString(),
                            FthUtility = fthUtility.ToString(),
                            SndUtility = sndUtility.ToString(),
                            CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        };
                        results.Add(result);
                    }
                }
                string json = JsonSerializer.Serialize(results, new JsonSerializerOptions {WriteIndented = true});
                return Ok(json);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        public async Task<IActionResult> GetPlayersID()
        {
            return Json(await _context.Players.Select(m => m.Id).ToListAsync());
        }

         public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players.OrderByDescending(d => d.Id).Take(10).Distinct().ToListAsync();
            return Json(players);
        }
        // GET: TwoPlayersStrategies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Strategies.ToListAsync());
        }

        // GET: TwoPlayersStrategies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategy = await _context.Strategies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strategy == null)
            {
                return NotFound();
            }

            return View(strategy);
        }

        // GET: TwoPlayersStrategies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TwoPlayersStrategies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FthPlayerID,SndPlayerID,FthPlayerStrategy,SndPlayerStrategy")] Strategy strategy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(strategy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(strategy);
        }

        [HttpPost]
        public async Task<IActionResult> PostStrategies([FromBody] List<Strategy> strategies)
        {
            try
            {
                foreach (var item in strategies)
                {
                    Strategy strategy = new Strategy()
                    {
                        FthPlayerID = item.FthPlayerID.ToString(),
                        SndPlayerID = item.SndPlayerID.ToString(),
                        FthPlayerStrategy = item.FthPlayerStrategy.ToString(),
                        SndPlayerStrategy = item.SndPlayerStrategy.ToString()
                    };
                    _context.Add(strategy);
                }
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        // GET: TwoPlayersStrategies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategy = await _context.Strategies.FindAsync(id);
            if (strategy == null)
            {
                return NotFound();
            }
            return View(strategy);
        }

        // POST: TwoPlayersStrategies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FthPlayerID,SndPlayerID,FthPlayerStrategy,SndPlayerStrategy")] Strategy strategy)
        {
            if (id != strategy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strategy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrategyExists(strategy.Id))
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
            return View(strategy);
        }

        // GET: TwoPlayersStrategies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strategy = await _context.Strategies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strategy == null)
            {
                return NotFound();
            }

            return View(strategy);
        }

        // POST: TwoPlayersStrategies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strategy = await _context.Strategies.FindAsync(id);
            _context.Strategies.Remove(strategy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrategyExists(int id)
        {
            return _context.Strategies.Any(e => e.Id == id);
        }
    }
}
