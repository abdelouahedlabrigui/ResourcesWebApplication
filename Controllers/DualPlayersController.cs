using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Games;
using ResourcesWebApplication.Models.Games.TwoPlayers;
using ResourcesWebApplication.Library.GamePlays;
using ResourcesWebApplication.Library.GamePlays.Models;

namespace ResourcesWebApplication.Controllers
{
    public class DualPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DualPlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GameFrame()
        {
            return View();
        }

        public async Task<IActionResult> GetReducedGameFrames(string url)
        {
            var playersDataset = await _context.PlayersDatasets.Take(20).OrderByDescending(_id => _id.Id).ToListAsync();
            if (playersDataset.Count() != 3)
            {
                return NotFound("May you please, delete all data in players dataset, and limit it to 3 of length.");
            }
            GameFrames gameFrames = new GameFrames();
            List<GameDualPlayers> reducedGameFramesResults = await gameFrames.GameFrame(url);            
            return Ok(reducedGameFramesResults);
        }
        public async Task<IActionResult> GetReducedGame(string url)
        {
            var playersDataset = await _context.PlayersDatasets.Take(20).OrderByDescending(_id => _id.Id).ToListAsync();
            if (playersDataset.Count() != 3)
            {
                return NotFound("May you please, delete all data in players dataset, and limit it to 3 of length.");
            }
            Games games = new Games();
            List<GameDualPlayers> reducedGameResults = await games.Game(url); 
            return Ok(reducedGameResults);
        }

        public async Task<IActionResult> GenerateParagraph(int fthPlayerID,int sndPlayerID)
        {
            var fthPlayer = await _context.Players.FirstOrDefaultAsync(d => d.Id == fthPlayerID);
            var sndPlayer = await _context.Players.FirstOrDefaultAsync(d => d.Id == sndPlayerID);

            var fthStrategy = await _context.PlayersDatasets.FirstOrDefaultAsync(d => d.Id == fthPlayerID);
            var sndStrategy = await _context.PlayersDatasets.FirstOrDefaultAsync(d => d.Id == sndPlayerID);
            
            int minimum = sndStrategy.Sum.Min();
            int maximum = sndStrategy.Sum.Max();
            
            var orderedSum = await _context.PlayersDatasets.OrderBy(d => d.Id).FirstOrDefaultAsync(d => d.Id == sndPlayerID);
            var filteredSum = await _context.PlayersDatasets.Where(n => int.Parse(n.Sum) > minimum).FirstOrDefaultAsync(d => d.Id == sndPlayerID);

            var selectedNumberStrategies = filteredSum.Sum.FirstOrDefault(d => d < maximum);

            string paragraph = $"{fthPlayer.Name} and {sndPlayer.Name} find themselves in a perplexing situation, unable to agree on where to dine for the evening. {fthPlayer.Name}, as Player 1, proposes a peculiar game to resolve their dilemma. {fthPlayer.Name} decides to secretly jot down either the number {fthStrategy.FthStrategy[0]}, {fthStrategy.FthStrategy[1]} or {fthStrategy.FthStrategy[2]}, while {sndPlayer.Name}, independently selects from the numbers {sndStrategy.SndStrategy[0]}, {sndStrategy.SndStrategy[1]} or {sndStrategy.SndStrategy[2]}. With their choices made, they reveal their selections to each other, intending to select a restaurant based on the sum of their chosen numbers. Should the sum amount to {minimum} or less, they opt for a ";
            return Ok();
        }


        public async Task<IActionResult> Plays()
        {
            
            return View(await _context.PlayersDatasets.Take(20).OrderByDescending(_id => _id.Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetPlays()
        {
            return Ok(await _context.PlayersDatasets.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> FetchPlayersDataset([FromBody] List<PlayersDataset> players)
        {
            try
            {
                foreach (var item in players)
                {
                    PlayersDataset player = new PlayersDataset()
                    {
                        FthPlayer = item.FthPlayer,
                        SndPlayer = item.SndPlayer,
                        FthStrategy = item.FthStrategy,
                        SndStrategy = item.SndStrategy,
                        Sum = item.Sum,
                        FthPreference = item.FthPreference,
                        SndPreference = item.SndPreference
                    };
                    _context.Add(player);
                }
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        public IActionResult GetGamesMatrix()
        {
            int[,] matrix = new int[3, 3] { { 3, 2, 1 }, { 2, 1, 3 }, { 1, 3, 2 } };

            // Antonia's strategies: 2, 4, 6
            // Bob's strategies: 1, 3, 5
            int[] antoniaStrategies = { 2, 4, 6 };
            int[] bobStrategies = { 1, 3, 5 };

            // Preferences
            Dictionary<char, int> antoniaPreferences = new Dictionary<char, int>() { { 'M', 3 }, { 'I', 2 }, { 'J', 1 } };
            Dictionary<char, int> bobPreferences = new Dictionary<char, int>() { { 'I', 3 }, { 'M', 2 }, { 'J', 1 } };

            // Calculating utilities and generating JSON
            List<object> results = new List<object>();

            foreach (var antoniaStrategy in antoniaStrategies)
            {
                foreach (var bobStrategy in bobStrategies)
                {
                    int sum = antoniaStrategy + bobStrategy;
                    char antoniaPreference = sum <= 5 ? 'M' : sum == 7 ? 'I' : 'J';
                    char bobPreference = sum == 7 ? 'I' : sum <= 5 ? 'M' : 'J';

                    int antoniaUtility = antoniaPreferences[antoniaPreference];
                    int bobUtility = bobPreferences[bobPreference];

                    var result = new
                    {
                        Antonia_Strategy = antoniaStrategy,
                        Bob_Strategy = bobStrategy,
                        Sum = sum,
                        Antonia_Preference = antoniaPreference,
                        Bob_Preference = bobPreference,
                        Antonia_Utility = antoniaUtility,
                        Bob_Utility = bobUtility
                    };

                    results.Add(result);
                }
            }

            // Outputting results in JSON format
            string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            return Ok(json);
        }



        [HttpGet]
        public async Task<IActionResult> GetPlayerById(string id)
        {
            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(m => m.Id == int.Parse(id));
                return Content(player != null ? string.Format("Id: {0}, Player: {1}", id.ToString(), player.Name) : null, "text/plain");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error loading file: {ex.Message}");
            }
        }
        public async Task<IActionResult> GetTables(string table)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(table) || table != "Players" || table != "Preferences" || table != "Strategies" || table != "Rules")
                {
                    return NotFound();
                }
                if (table == "Players")
                {
                    var players = await _context.Players.Take(7).Distinct().OrderByDescending(d => d.Id).ToListAsync();
                    if (players.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(players);
                } else if (table == "Preferences")
                {
                    var prefrences = await _context.Preferences.Take(7).OrderByDescending(d => d.Id).Distinct().ToListAsync();
                    if (prefrences.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(prefrences);
                } else if (table == "Strategies")
                {
                    var strategies = await _context.Strategies.Take(7).OrderByDescending(d => d.Id).Distinct().ToListAsync();
                    if (strategies.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(strategies);
                } else if (table == "Rules")
                {
                    var rules = await _context.Rules.Take(7).OrderByDescending(d => d.Id).Distinct().ToListAsync();
                    if (rules.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(rules);
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        public async Task<IActionResult> GetPlayers()
        {
            try
            {                
                var players = await _context.Players.Take(7).Distinct().OrderByDescending(d => d.Id).ToListAsync();
                if (players.Count == 0)
                {
                    return NoContent();
                }
                return Ok(players);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        public async Task<IActionResult> GetPreferences()
        {
            try
            {
                var prefrences = await _context.Preferences.Take(7).OrderByDescending(d => d.Id).Distinct().ToListAsync();
                if (prefrences.Count == 0)
                {
                    return NoContent();
                }
                return Ok(prefrences);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        public async Task<IActionResult> GetStrategies()
        {
            try
            {
                var strategies = await _context.Strategies.Take(7).OrderByDescending(d => d.Id).Distinct().ToListAsync();
                if (strategies.Count == 0)
                {
                    return NoContent();
                }
                return Ok(strategies);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        public async Task<IActionResult> GetRules()
        {
            try
            {
                var rules = await _context.Rules.Take(7).OrderByDescending(d => d.Id).Distinct().ToListAsync();
                if (rules.Count == 0)
                {
                    return NoContent();
                }
                return Ok(rules);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        // GET: DualPlayers
        public async Task<IActionResult> Index()
        {
            return View(await _context.DualPlayers.ToListAsync());
        }

        // GET: DualPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dualPlayers = await _context.DualPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dualPlayers == null)
            {
                return NotFound();
            }

            return View(dualPlayers);
        }

        // GET: DualPlayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // [HttpGet]
        // public async Task<IActionResult> PostGame(string fthPlayerID, string sndPlayerID,  string preferenceId1, string preferenceId2, string preferenceId3, string strategyId1, string strategyId2, string strategyId3, string rule1, string rule2)
        // {
        //     try
        //     {
        //         int[] fthStrategies = {
        //             int.Parse(strategyId1.Split(',')[0]),
        //             int.Parse(strategyId2.Split(',')[0]),
        //             int.Parse(strategyId3.Split(',')[0])
        //         };

        //         int[] sndStrategies = {
        //             int.Parse(strategyId1.Split(',')[1]),
        //             int.Parse(strategyId2.Split(',')[1]),
        //             int.Parse(strategyId3.Split(',')[1])
        //         };

        //         Dictionary<string, int> fthPreferences = new Dictionary<string, int>()
        //         {
        //             {preferenceId1.Split(',')[0], 3},
        //             {preferenceId2.Split(',')[0], 2},
        //             {preferenceId3.Split(',')[0], 1},
        //         };

        //         Dictionary<string, int> sndPreferences = new Dictionary<string, int>()
        //         {
        //             {preferenceId1.Split(',')[1], 3},
        //             {preferenceId2.Split(',')[1], 2},
        //             {preferenceId3.Split(',')[1], 1},
        //         };
                
        //         // List<object> results = new List<object>();
        //         foreach (var fthStrategyItem in fthStrategies)
        //         {
        //             foreach (var SndStrategyItem in sndStrategies)
        //             {
        //                 int sum = fthStrategyItem + SndStrategyItem;
        //                 string fthPreferenceProcess = sum <= int.Parse(rule1) ? preferenceId1.Split(',')[0] : sum == int.Parse(rule2) ? preferenceId2.Split(',')[0] : preferenceId3.Split(',')[1];
        //                 string sndPreferenceProcess = sum == int.Parse(rule2) ? preferenceId1.Split(',')[1] : sum <= int.Parse(rule1) ? preferenceId2.Split(',')[1] : preferenceId3.Split(',')[0];

        //                 int fthUtility = fthPreferences[fthPreferenceProcess];
        //                 int sndUtility = sndPreferences[sndPreferenceProcess];

        //                 DualPlayers result = new DualPlayers() 
        //                 {
        //                     FthPlayerID = fthPlayerID,
        //                     SndPlayerID = sndPlayerID,
        //                     FthStrategy = fthStrategyItem.ToString(),
        //                     SndStrategy = SndStrategyItem.ToString(),
        //                     Sum = sum.ToString(),
        //                     FthPreference = fthPreferenceProcess,
        //                     SndPreference = sndPreferenceProcess,
        //                     FthUtility = fthUtility.ToString(),
        //                     SndUtility = sndUtility.ToString(),
        //                     CreatedAT = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.fff")
        //                 };
        //                 _context.Add(result);
        //             }
        //         }
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }            
        // }
        [HttpGet]
        public async Task<IActionResult> PostGame(string fthPlayerID, string sndPlayerID,  string preferenceId1, string preferenceId2, string preferenceId3, string strategyId1, string strategyId2, string strategyId3, string rule1, string rule2)
        {
            try
            {
                int[] fthStrategies = {
                    int.Parse(strategyId1.Split(',')[0]),
                    int.Parse(strategyId2.Split(',')[0]),
                    int.Parse(strategyId3.Split(',')[0])
                };

                int[] sndStrategies = {
                    int.Parse(strategyId1.Split(',')[1]),
                    int.Parse(strategyId2.Split(',')[1]),
                    int.Parse(strategyId3.Split(',')[1])
                };

                Dictionary<string, int> fthPreferences = new Dictionary<string, int>()
                {
                    {preferenceId1.Split(',')[0], 3},
                    {preferenceId2.Split(',')[0], 2},
                    {preferenceId3.Split(',')[0], 1},
                };

                Dictionary<string, int> sndPreferences = new Dictionary<string, int>()
                {
                    {preferenceId1.Split(',')[1], 3},
                    {preferenceId2.Split(',')[1], 2},
                    {preferenceId3.Split(',')[1], 1},
                };
                
                // List<object> results = new List<object>();
                foreach (var fthStrategyItem in fthStrategies)
                {
                    foreach (var SndStrategyItem in sndStrategies)
                    {
                        int sum = fthStrategyItem + SndStrategyItem;
                        string fthPreferenceProcess = sum <= int.Parse(rule1) ? preferenceId1.Split(',')[0] : sum == int.Parse(rule2) ? preferenceId2.Split(',')[0] : preferenceId3.Split(',')[0];
                        string sndPreferenceProcess = sum <= int.Parse(rule2) ? preferenceId1.Split(',')[1] : sum == int.Parse(rule1) ? preferenceId2.Split(',')[1] : preferenceId3.Split(',')[1];


                        int fthUtility = fthPreferences[fthPreferenceProcess];
                        int sndUtility = sndPreferences[sndPreferenceProcess];

                        DualPlayers result = new DualPlayers() 
                        {
                            FthPlayerID = fthPlayerID,
                            SndPlayerID = sndPlayerID,
                            FthStrategy = fthStrategyItem.ToString(),
                            SndStrategy = SndStrategyItem.ToString(),
                            Sum = sum.ToString(),
                            FthPreference = fthPreferenceProcess,
                            SndPreference = sndPreferenceProcess,
                            FthUtility = fthUtility.ToString(),
                            SndUtility = sndUtility.ToString(),
                            CreatedAT = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.fff")
                        };
                        _context.Add(result);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // POST: DualPlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FthPlayerID,SndPlayerID,FthStrategy,SndStrategy,Sum,FthPreference,SndPreference,FthUtility,SndUtility,CreatedAT")] DualPlayers dualPlayers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(dualPlayers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(dualPlayers);
            }
            catch (System.Exception ex)
            {
                return StatusCode(417, ex.Message);
            }
        }

        // GET: DualPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dualPlayers = await _context.DualPlayers.FindAsync(id);
            if (dualPlayers == null)
            {
                return NotFound();
            }
            return View(dualPlayers);
        }

        // POST: DualPlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FthStrategy,SndStrategy,Sum,FthPreference,SndPreference,FthUtility,SndUtility,CreatedAT")] DualPlayers dualPlayers)
        {
            if (id != dualPlayers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dualPlayers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DualPlayersExists(dualPlayers.Id))
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
            return View(dualPlayers);
        }

        // GET: DualPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dualPlayers = await _context.DualPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dualPlayers == null)
            {
                return NotFound();
            }

            return View(dualPlayers);
        }

        // POST: DualPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dualPlayers = await _context.DualPlayers.FindAsync(id);
            _context.DualPlayers.Remove(dualPlayers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAllPlays()
        {
            _context.PlayersDatasets.RemoveRange();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DualPlayersExists(int id)
        {
            return _context.DualPlayers.Any(e => e.Id == id);
        }
    }
}
