using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.MachineLearning;
using ResourcesWebApplication.Models.MachineLearning.Visualizations;
using ResourcesWebApplication.Models.MachineLearning.Astronomy;
using ResourcesWebApplication.Models.MachineLearning.Astronomy.Problems;
using ResourcesWebApplication.Models.Documents;
using System.IO;
using ResourcesWebApplication.Models.LanguageProcessing;
using Microsoft.AspNetCore.Http;
using ResourcesWebApplication.Models.Dictionaries;
using ResourcesWebApplication.Models.LanguageProcessing.QueryProcessing;
using ResourcesWebApplication.Models.LanguageProcessing.Visualization;
using ResourcesWebApplication.Models.MachineLearning.Datasets;
using ResourcesWebApplication.Models.Jira;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Net.Http;
using System.Net.Http.Json;
using ResourcesWebApplication.Models.Endpoints;
using ResourcesWebApplication.Models.MachineLearning.Weather;
using ResourcesWebApplication.Models.MachineLearning.Aviation;
using ResourcesWebApplication.Models.MachineLearning.Aviation.Datasets;
using ResourcesWebApplication.Models.GenerativeAI.Biography;
using ResourcesWebApplication.Models.Aerospace;

namespace ResourcesWebApplication.Controllers.MachineLearning
{
    [Route("[controller]")]
    public class MachineLearningController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public MachineLearningController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }
        [HttpGet]
        [Route("Get200OKSatusTest")]
        public IActionResult Get200OKSatusTest()
        {
            return Ok();
        }
        [HttpGet]
        [Route("GetTopDescConcept")]
        public async Task<IActionResult> GetTopDescConcept()
        {
            var concept = await _context.AircraftStructures
                .Take(1)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            if (concept.Count == 0)
            {
                return NoContent();
            }
            return Ok(concept);
        }
        [HttpGet]
        [Route("GetTopDescConceptDefinition")]
        public async Task<IActionResult> GetTopDescConceptDefinition()
        {
            var concept = await _context.AircraftStructuresConceptDefinitions
                .Take(1)
                .OrderByDescending(o => o.Id)
                .Select(s => new AircraftStructuresConceptDefinition {
                    Id = s.Id,
                    Concept = s.Concept,
                    Definition = s.Definition.Replace("failed to get console mode for stdout: The handle is invalid. failed to get console mode for stderr: The handle is invalid.", ""),
                    CreatedAT = s.CreatedAT                    
                })
                .ToListAsync();
            if (concept.Count == 0)
            {
                return NoContent();
            }
            return Ok(concept);
        }
        [HttpGet]
        [Route("PostAircraftStructureGeneratedDefinition")]
        public async Task<IActionResult> PostAircraftStructureGeneratedDefinition(string concept, string definition)
        {
            try
            {
                AircraftStructuresConceptDefinition conceptDefinition = new AircraftStructuresConceptDefinition{
                    Concept = concept,
                    Definition = definition,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                var query = await _context.AircraftStructuresConceptDefinitions
                    .Where(w => w.Concept == concept)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    
                    _context.AircraftStructuresConceptDefinitions.Add(conceptDefinition);
                    await _context.SaveChangesAsync();
                    return Ok(new {Message = "Data saved successfully."});
                }
                return Ok(new {Message = "No change"});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("PostAircraftStructureConcept")]
        public async Task<IActionResult> PostAircraftStructureConcept(string concept)
        {
            try
            {
                AircraftStructure aircraftStructure = new AircraftStructure
                {
                    Concept = concept,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                var query = await _context.AircraftStructures
                    .Where(w => w.Concept == concept)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    
                    _context.AircraftStructures.Add(aircraftStructure);
                    await _context.SaveChangesAsync();
                    return Ok(new {Message = "Data saved successfully."});
                }                    
                return Ok(new {Message = "No change."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        // 79351
        [HttpGet]
        [Route("GetAviationPioneersDatasetNames")]
        public async Task<IActionResult> GetAviationPioneersDatasetNames()
        {
            try
            {
                var query = await _context.Entities
                    .Where(w => w.Id >= 79351)
                    .Where(w => w.Id <= 121740)
                    .Select(s => s.DatasetName)
                    .Distinct()
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
        [Route("GetPersonLifeFromEntitiesByDatasetName")]
        public async Task<IActionResult> GetPersonLifeFromEntitiesByDatasetName(string datasetName)
        {
            try
            {
                var query = await _context.Entities
                    .Where(w => w.DatasetName == datasetName)
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
        [Route("GetPersonLifeByYearsByDatasetName")]
        public async Task<IActionResult> GetPersonLifeByYearsByDatasetName(string datasetName)
        {
            try
            {
                var query = await _context.Lives
                    .Where(w => w.DatasetName == datasetName)
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
        public async Task<object> AutomatePostingPersonsLivesByYears(string datasetName, int birth, int death)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName) || birth >= death || birth.ToString().Length != 4 || death.ToString().Length != 4)
                {
                    return Ok(new {Message = $"Wrong inputs."});
                }
                // Query to fetch and filter data
                var entities = await _context.Entities
                    .Where(w => w.Label == "DATE" && w.Text.Length == 4 && w.DatasetName == datasetName)
                    .ToListAsync(); // Move the query to SQL and bring the result into memory

                // Process the entities in memory (client-side)
                var query = entities
                    .Where(w => int.TryParse(w.Text, out int year) && year >= birth && year <= death)
                    .GroupBy(g => g.Text)
                    .Select(s => new Life { DatasetName = datasetName, Year = s.Key, Count = s.Count() })
                    .ToList();

                // Handle if no results
                if (query.Count == 0)
                {
                    return new { Message = "Query returned no results." };
                }

                // Save each item to the database
                foreach (var item in query)
                {
                    _context.Lives.Add(item);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return new { Message = "Data saved to database." };
            }
            catch (System.Exception ex)
            {
                // Return a 500 Internal Server Error for exceptions
                return new { Message = "An error occurred.", Error = ex.Message };
            }
        }
        [HttpGet]
        [Route("RequestAutomatePostingPersonsLivesByYears")]
        public async Task<IActionResult> RequestAutomatePostingPersonsLivesByYears()
        {
            try
            {
                var query = await _context.PersonLifespans
                    .ToListAsync();
                foreach (var item in query)
                {
                    await AutomatePostingPersonsLivesByYears(item.DatasetName, item.Birth, item.Death);
                }
                return Ok(new {Message = "Data saved."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("PostPersonBirthAndDeathDates")]
        public async Task<IActionResult> PostPersonBirthAndDeathDates()
        {
            try
            {
                List<PersonLifespan> lives = new List<PersonLifespan>();
                var lifeSpan = new PersonLifespan { 
                    DatasetName = "Jacqueline_Cochran.txt", Birth = 1906, Death = 1980 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Blanche_Stuart_Scott.txt", Birth = 1885, Death = 1970 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Wilbur_Wright.txt", Birth = 1867, Death = 1948 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Antoine_de_Saint-Exupery.txt", Birth = 1900, Death = 1944 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Wiley_Post.txt", Birth = 1898, Death = 1935 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Eddie_Rickenbacker.txt", Birth = 1890, Death = 1973 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Bessie_Coleman.txt", Birth = 1892, Death = 1926 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Douglas_Bader.txt", Birth = 1910, Death = 1982 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Alberto_Santos-Dumont.txt", Birth = 1873, Death = 1932 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Glenn_Curtiss.txt", Birth = 1878, Death = 1930 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Otto_Lilienthal.txt", Birth = 1848, Death = 1896 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Frank_Whittle.txt", Birth = 1907, Death = 1996 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Richard_Pearse.txt", Birth = 1877, Death = 1953 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Hugo_Junkers.txt", Birth = 1859, Death = 1935 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Elinor_Smith.txt", Birth = 1911, Death = 2010 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Clyde_Cessna.txt", Birth = 1879, Death = 1954 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Harriet_Quimby.txt", Birth = 1875, Death = 1912 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Jean_Mermoz.txt", Birth = 1901, Death = 1936 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Paul_Cornu.txt", Birth = 1881, Death = 1944 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Jimmy_Doolittle.txt", Birth = 1896, Death = 1993 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Igor_Sikorsky.txt", Birth = 1889, Death = 1972 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Ruth_Law.txt", Birth = 1887, Death = 1970 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Louis_Bleriot.txt", Birth = 1872, Death = 1936 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Charles_Lindbergh.txt", Birth = 1902, Death = 1974 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Geoffrey_de_Havilland.txt", Birth = 1882, Death = 1965 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "James_H_Doolittle.txt", Birth = 1896, Death = 1993 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Jack_Northrop.txt", Birth = 1895, Death = 1981 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Howard_Hughes.txt", Birth = 1905 , Death = 1976 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Ernst_Heinkel.txt", Birth = 1888, Death = 1958 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Amelia_Earhart.txt", Birth = 1897, Death = 1939 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "John_Stringfellow.txt", Birth = 1799, Death = 1883 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Ladislao_Pazmany.txt", Birth = 1923, Death = 2006 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Roscoe_Turner.txt", Birth = 1895, Death = 1970 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Adolphe_Pegoud.txt", Birth = 1889, Death = 1915 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Iven_Kincheloe.txt", Birth = 1928, Death = 1958 };                
                lives.Add(lifeSpan);
                lifeSpan = new PersonLifespan { 
                    DatasetName = "Orville_Wright.txt", Birth = 1871, Death = 1948 };                
                lives.Add(lifeSpan);
                foreach (var item in lives)
                {
                    PersonLifespan lifespan = new PersonLifespan()
                    {
                        DatasetName = item.DatasetName,
                        Birth = item.Birth,
                        Death = item.Death
                    };
                    _context.PersonLifespans.Add(lifespan);
                }
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Data saved."});
            }
            catch (System.Exception ex)
            {
                // Return a 500 Internal Server Error for exceptions
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }
        }
        [HttpGet]
        [Route("LocalPostPersonLifeByYears")]
        public async Task<IActionResult> LocalPostPersonLifeByYears()
        {
            try
            {
                // Check if ModelState is valid
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Message = "Model not valid", Errors = errors });
                }
                
                // Query to fetch and filter data
                var entities = await _context.Entities
                    .Where(w => w.Label == "DATE" && w.Text.Length == 4 && w.DatasetName == "Amelia_Earhart.txt")
                    .ToListAsync(); // Move the query to SQL and bring the result into memory

                // Process the entities in memory (client-side)
                var query = entities
                    .Where(w => int.TryParse(w.Text, out int year) && year >= 1897 && year <= 1939)
                    .GroupBy(g => g.Text)
                    .Select(s => new Life { DatasetName = "Amelia_Earhart.txt", Year = s.Key, Count = s.Count() })
                    .ToList();

                // Handle if no results
                if (query.Count == 0)
                {
                    return Ok(new { Message = "Query returned no results." });
                }

                // Save each item to the database
                foreach (var item in query)
                {
                    _context.Lives.Add(item);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Data saved to database." });
            }
            catch (System.Exception ex)
            {
                // Return a 500 Internal Server Error for exceptions
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }
        }
        private async Task<List<NounChunkByYear>> PostNounChunksByYearAndPerson(string datasetName, string year)
        {
            
            var query = await _context.NounChunks
                .Where(w => w.DatasetName == datasetName)
                .Where(w => w.Text.Contains($" {year} "))
                .ToListAsync();
            
            List<NounChunkByYear> nounChunkByYearList = new List<NounChunkByYear>();
            foreach (var item in query)
            {
                var chunkByYear = new NounChunkByYear()
                {
                    DatasetName = datasetName,
                    Year = year,
                    Text = item.Text,
                    RootText = item.RootText,
                    RootDep = item.RootDep,
                    RootHead = item.RootHead
                };
                _context.NounChunkByYears.Add(chunkByYear);
                nounChunkByYearList.Add(chunkByYear);
            }
            await _context.SaveChangesAsync();
            return nounChunkByYearList;
        }
        [HttpGet]
        [Route("RequestAutomatePostingNounChunksByYearAndPerson")]
        public async Task<IActionResult> RequestAutomatePostingNounChunksByYearAndPerson()
        {
            try
            {
                await GetParseTreesByNCRootHeadAndDatasetName();
                
                return Ok(new {Message = "Data saved."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        private async Task<List<NounChunkByYear>> GetNounChunksByYearAndPerson(string year, string datasetName)
        {
            if (string.IsNullOrWhiteSpace(year) || year.Length != 4 || !datasetName.Contains(".txt"))
            {
                throw new Exception("Search failed");
            }
            List<NounChunkByYear> nounChunkByYearList = new List<NounChunkByYear>();
            var query = await _context.NounChunks
                .Where(w => w.DatasetName == datasetName)
                .Where(w => w.Text.Contains($" {year} "))
                .ToListAsync();
            if (query.Count == 0)
            {
                throw new Exception($"Query Count: {query.Count}");
            }
            foreach (var item in query)
            {
                var chunkByYear = new NounChunkByYear()
                {
                    DatasetName = datasetName,
                    Year = year,
                    Text = item.Text,
                    RootText = item.RootText,
                    RootDep = item.RootDep,
                    RootHead = item.RootHead
                };
                nounChunkByYearList.Add(chunkByYear);
            }
            return nounChunkByYearList;
        }
        private async Task<List<ParseTree>> GetParseTreeByRootHeadAndPerson(string root_head, string datasetName)
        {
            // if (string.IsNullOrWhiteSpace(root_head) || !datasetName.Contains(".txt"))
            // {
            //     throw new Exception("Search failed");
            // }
            var query = await _context.ParseTrees
                .Where(w => w.DatasetName == datasetName)
                .Where(w => w.Text.Contains($"{root_head}"))
                .Where(w => w.Child != "[]")
                .ToListAsync();
            // if (query.Count == 0)
            // {
            //     throw new Exception($"Query Count: {query.Count}");
            // }
            return query;
        }
        private async Task<List<NounChunk>> GetParseTreesByNCRootHeadAndDatasetName()
        {
            try
            {
                List<NounChunk> allChunks = new List<NounChunk>();
                List<ParseTreeByYear> parseTreeByYearList = new List<ParseTreeByYear>();
                List<NounChunkByYear> nounChunkByYearList = new List<NounChunkByYear>();
                
                var query = await _context.Lives 
                    .Where(w => w.DatasetName != "Amelia_Earhart.txt")                   
                    .ToListAsync();
                foreach (var item in query)
                {
                    await PostNounChunksByYearAndPerson(item.DatasetName,item.Year);
                }
                List<NounChunkByYear> queryNounChunks = await _context.NounChunkByYears
                    .Where(w => w.DatasetName != "Amelia_Earhart.txt")
                    .ToListAsync();
                nounChunkByYearList.AddRange(queryNounChunks);
                foreach (var item in nounChunkByYearList)
                {
                    var parseTrees = await GetParseTreeByRootHeadAndPerson(item.RootHead, item.DatasetName);
                    foreach (var pt in parseTrees)
                    {                        
                        var treeByYear = new ParseTreeByYear()
                        {
                            DatasetName = item.DatasetName,
                            Year = item.Year,
                            RootHead = item.RootHead,
                            Text = pt.Text,
                            Dep = pt.Dep,
                            HeadText = pt.HeadText,
                            HeadPos = pt.HeadPos,
                            Child = pt.Child,
                            CreatedAT = pt.CreatedAT
                        };
                        parseTreeByYearList.Add(treeByYear);
                    }
                }
                // // Add all NounChunkByYears and ParseTreeByYears in one batch
                _context.NounChunkByYears.AddRange(nounChunkByYearList.Distinct());
                _context.ParseTreeByYears.AddRange(parseTreeByYearList.Distinct());

                // Save all changes to the database at once
                await _context.SaveChangesAsync();

                return new List<NounChunk>();
            }
            catch (Exception ex)
            {
                // Log the error (for debugging or monitoring)
                Console.WriteLine($"Error: {ex.Message}");
                return null;  // Or handle the error based on your logic (e.g., return an empty list)
            }
        }
        private async Task<List<string>> GetParseTreesChildrenListFormat(string datasetName)
        {
            try
            {
                // if (string.IsNullOrWhiteSpace(datasetName))
                // {
                //     throw new Exception("Null value");
                // }
                var query = await GetParseTreeByYear(datasetName);
                List<string> children = new List<string>();
                foreach (var item in query)
                {
                    var split = item.Child.Split(',');
                    children.AddRange(split);
                }
                List<string> childrenList = new List<string>(); 
                children = children.Distinct().ToList();
                foreach (var item in children)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        childrenList.Add(item.Replace(" ", ""));
                    }
                }
                return childrenList.Distinct().ToList();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<List<ChildCollect>> GetParseTreesChildrenListFormatWithDatasetNames(string datasetName)
        {
            try
            {
                // if (string.IsNullOrWhiteSpace(datasetName))
                // {
                //     throw new Exception("Null value");
                // }
                var query = await GetParseTreeByYear(datasetName);
                List<ChildCollect> children = new List<ChildCollect>();
                foreach (var item in query)
                {
                    var childSplit = item.Child.Split(',');
                    foreach (var child in childSplit)
                    {
                        ChildCollect childCollect = new ChildCollect 
                        {
                            DatasetName = datasetName, Child = child
                        };                        
                        children.Add(childCollect);
                    }
                }
                List<ChildCollect> childrenList = new List<ChildCollect>(); 
                children = children.Distinct().ToList();
                foreach (var item in children)
                {
                    if (!string.IsNullOrWhiteSpace(item.Child))
                    {
                        ChildCollect childCollect = new ChildCollect 
                        {
                            DatasetName = datasetName, Child = item.Child.Replace(" ", "")
                        };                        
                        children.Add(childCollect);
                    }
                }
                return childrenList.Distinct().ToList();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetChildrenFromDatasets")]
        public async Task<IActionResult> GetChildrenFromDatasets()
        {
            var query = await _context.Entities
                    .Where(w => w.Id >= 79351)
                    .Where(w => w.Id <= 121740)
                    .Select(s => s.DatasetName)
                    .Distinct()
                    .ToListAsync();
            List<object> children = new List<object>(); 
            foreach (var item in query)
            {
                children.AddRange(await GetParseTreesChildrenListFormat(item));
            }
            return Ok(children);
        }
        private async Task<List<ChildNounChunkSearch>> GetChildNounChunkSearchesAsync(string child, string datasetName)
        {
            try
            {
                // if (string.IsNullOrWhiteSpace(datasetName))
                // {
                //     throw new Exception("Null value");
                // }
                // var query = await GetParseTreesChildrenListFormat(datasetName);
                var chunks = await _context.NounChunks
                    .Where(w => w.DatasetName == datasetName)
                    .Where(w => w.RootHead == $"{child}")
                    .Where(w => w.RootDep == "dobj")
                    .Select(s => new ChildNounChunkSearch { 
                        Id = s.Id,
                        DatasetName = s.DatasetName,
                        Text = s.Text,
                        RootText = s.RootText,
                        RootDep = s.RootDep,
                        RootHead = s.RootHead,
                        Child = $"{child}"
                        })
                    .ToListAsync();
                return chunks;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        private async Task<List<ParseTreeByYear>> GetParseTreeByYear(string datasetName)
        {
            try
            {
                // if (string.IsNullOrWhiteSpace(datasetName))
                // {
                //     throw new Exception("Null value");
                // }
                var query = await _context.ParseTreeByYears
                    .Where(w => w.DatasetName == datasetName)
                    .Select(s => new ParseTreeByYear {
                        Id = s.Id,
                        DatasetName = s.DatasetName,
                        Year = s.Year,
                        RootHead = s.RootHead,
                        Text = s.Text,
                        Dep = s.Dep,
                        HeadText = s.HeadText,
                        HeadPos = s.HeadPos,
                        Child = s.Child.Replace("[", "").Replace("]", "").Replace(")", "")
                            .Replace("(", "").Replace(".", "").Replace(", ,", ","),
                        CreatedAT = s.CreatedAT,
                    })
                    .ToListAsync();
                // if (query.Count == 0)
                // {
                //     throw new Exception($"Query Count {query.Count}.");
                // }
                return query;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<List<NounChunkByYear>> GetNounChunksByYear(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    throw new Exception("Null value");
                }
                var query = await _context.NounChunkByYears
                    .Where(w => w.DatasetName == datasetName)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    throw new Exception($"Query Count {query.Count}.");
                }
                return query;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<List<ChildNounChunkSearch>> AutomateSearchingChildNounChunkSearchesAsync()
        {
            try
            {
                var query = await _context.Lives
                    .Where(w => w.DatasetName != "Amelia_Earhart.txt")
                    .Select(s => s.DatasetName)
                    .Distinct()
                    .ToListAsync();
                List<ChildNounChunkSearch> searches = new List<ChildNounChunkSearch>();
                // foreach (var item in query)
                // {
                var children = await GetParseTreesChildrenListFormat("Jacqueline_Cochran.txt"); 
                foreach (var child in children)
                {
                    var chunks = await GetChildNounChunkSearchesAsync(child, "Jacqueline_Cochran.txt");
                    searches.AddRange(chunks);
                } 
                // }
                return searches;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("RequestAutomateSearchingChildNounChunkSearchesAsync")]
        public async Task<IActionResult> RequestAutomateSearchingChildNounChunkSearchesAsync()
        {
            var query = await AutomateSearchingChildNounChunkSearchesAsync();
            return Ok(query);
        }
        // GetChildNounChunkSearchesAsync
        [HttpGet]
        [Route("SearchGetChildNounChunkSearchesAsync")]
        public async Task<IActionResult> SearchGetChildNounChunkSearchesAsync(string datasetName)
        {
            try
            {                
                List<ChildNounChunkSearch> searches = new List<ChildNounChunkSearch>();
                var lives = await _context.Lives
                    .Where(w => w.DatasetName == datasetName)
                    .Select(s => s.DatasetName)
                    .Distinct()
                    .ToListAsync();
                var query = await GetParseTreesChildrenListFormat(datasetName);  
                foreach (var item in query)
                {
                    var chunks = await GetChildNounChunkSearchesAsync(item, datasetName);
                    searches.AddRange(chunks);
                }              
                return Ok(searches);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("SearchGetParseTreeByYear")]
        public async Task<IActionResult> SearchGetParseTreeByYear(string datasetName)
        {
            try
            {                
                var query = await GetParseTreeByYear(datasetName);                
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("SearchGetNounChunksByYear")]
        public async Task<IActionResult> SearchGetNounChunksByYear(string datasetName)
        {
            try
            {                
                var query = await GetNounChunksByYear(datasetName);
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        // GetBiographyQuestionsAsync
        [HttpGet]
        [Route("SearchBiographyQuestionsAsync")]
        public async Task<IActionResult> SearchBiographyQuestionsAsync()
        {
            try
            {                
                var query = await GetBiographyQuestionsAsync();
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        private async Task<List<BiographyQuestion>> GetBiographyQuestionsAsync()
        {
            try
            {
                List<BiographyQuestion> biographies = new List<BiographyQuestion>();              
                var datasetNames = await _context.BiographyQuestions
                    .Where(w => w.DatasetName != "Amelia_Earhart.txt")
                    .Select(s => s.DatasetName)
                    .Distinct()
                    .ToListAsync();
                foreach (var datasetName in datasetNames)
                {
                    var query = await _context.BiographyQuestions
                    .Where(w => w.DatasetName == datasetName)
                    .Select(w => new BiographyQuestion {
                        Id = w.Id,
                        DatasetName = w.DatasetName,
                        Person = w.Person,
                        Prompt = w.Prompt,
                        Question = w.Question.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace("\n", " "),
                    })
                    .ToListAsync();
                
                    foreach (var item in query)
                    {
                        var questions = item.Question.Split('?');
                        foreach (var question in questions)
                        {                        
                            BiographyQuestion biographyQuestion = new BiographyQuestion()
                            {
                                Id = item.Id,
                                DatasetName = item.DatasetName,
                                Person = item.Person,
                                Prompt = item.Prompt,
                                Question = $"{question}?",
                            };
                            biographies.Add(biographyQuestion);
                        }
                    }                    
                }                
                return biographies.Distinct().ToList();
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("PostLifeExperience")]
        public async Task<IActionResult> PostLifeExperience([FromBody] LifeExperience experience) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                experience.Happened.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace("\n", " ");
                _context.LifeExperiences.Add(experience);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostBiographyQuestionAnswer")]
        public async Task<IActionResult> PostBiographyQuestionAnswer([FromBody] BiographyQuestionAnswer answer) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                answer.Answer.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace("\n", " ");
                _context.BiographyQuestionAnswers.Add(answer);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        private async Task<List<EventYear>> GetEventYears()
        {
            try
            {
                var query = await _context.EventYears
                    .Take(5)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    throw new Exception($"Query Count: {query.Count}");
                }
                return query;
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [Route("SearchToGetEventYears")]
        public async Task<IActionResult> SearchToGetEventYears()
        {
            var query = await GetEventYears();
            return Ok(query);
        }
        [HttpPost]
        [Route("PostEventYears")]
        public async Task<IActionResult> PostEventYears([FromBody] EventYear eventYear) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.EventYears.Add(eventYear);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostPersonBiographyQuestions")]
        public async Task<IActionResult> PostPersonBiographyQuestions([FromBody] BiographyQuestion question) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.BiographyQuestions.Add(question);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostPersonLifeByYears")]
        public async Task<IActionResult> PostPersonLifeByYears([FromBody] Life life) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.Lives.Add(life);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetPersonShortBiographyFullName")]
        public async Task<IActionResult> GetPersonShortBiographyFullName()
        {
            try
            {
                var query = await _context.Persons 
                    .Select(s => s.FullName)
                    .Distinct()
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
        [Route("GetPersonShortBiography")]
        public async Task<IActionResult> GetPersonShortBiography()
        {
            try
            {
                var query = await _context.Persons 
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
        [Route("GetPersonShortBiographyByFullName")]
        public async Task<IActionResult> GetPersonShortBiographyByFullName(string full_name)
        {
            try
            {
                if (string.IsNullOrEmpty(full_name))
                {
                    return Ok(new {Message = $"Value null or empty."});
                }
                var query = await _context.Persons 
                    .Where(w => w.FullName == full_name)
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
        [HttpPost]
        [Route("PostPersonShortBiography")]
        public async Task<IActionResult> PostPersonShortBiography([FromBody] Person person) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostRoutesDataset")]
        public async Task<IActionResult> PostRoutesDataset([FromBody] Route route) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.Routes.Add(route);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostAirportsDataset")]
        public async Task<IActionResult> PostAirportsDataset([FromBody] Airport airport) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.Airports.Add(airport);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetAirports")]
        public async Task<IActionResult> GetAirports()
        {
            try
            {
                var query = await _context.Airports 
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
        [Route("GetAccidents")]
        public async Task<IActionResult> GetAccidents()
        {
            try
            {
                var query = await _context.Accidents 
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
        [HttpPost]
        [Route("PostAccidentsDataset")]
        public async Task<IActionResult> PostAccidentsDataset([FromBody] Accident accident) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.Accidents.Add(accident);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        
        // FeatureImportances BestHyperParameters ClassificationReportings
        [HttpPost]
        [Route("PostFeatureImportances")]
        public async Task<IActionResult> PostFeatureImportances([FromBody] FeatureImportance featureImportance) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.FeatureImportances.Add(featureImportance);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetFeatureImportancesByDatasetName")]
        public async Task<IActionResult> GetFeatureImportancesByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.FeatureImportances 
                    .Where(w => w.DatasetName == datasetName)
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
        // SVMBestHyperParameter
        [HttpPost]
        [Route("PostSVMBestHyperParameters")]
        public async Task<IActionResult> PostSVMBestHyperParameters([FromBody] SVMBestHyperParameter bestHyperParameter) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.SVMBestHyperParameters.Add(bestHyperParameter);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetSVMBestHyperParametersByDatasetName")]
        public async Task<IActionResult> GetSVMBestHyperParametersByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.SVMBestHyperParameters 
                    .Where(w => w.DatasetName == datasetName)
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
        [HttpPost]
        [Route("PostBestHyperParameters")]
        public async Task<IActionResult> PostBestHyperParameters([FromBody] BestHyperParameter bestHyperParameter) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.BestHyperParameters.Add(bestHyperParameter);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetBestHyperParametersByDatasetName")]
        public async Task<IActionResult> GetBestHyperParametersByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.BestHyperParameters 
                    .Where(w => w.DatasetName == datasetName)
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
        [HttpPost]
        [Route("PostClassificationReportings")]
        public async Task<IActionResult> PostClassificationReportings([FromBody] ClassificationReporting classificationReporting) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.ClassificationReportings.Add(classificationReporting);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetClassificationReportingsByDatasetName")]
        public async Task<IActionResult> GetClassificationReportingsByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.ClassificationReportings 
                    .Where(w => w.DatasetName == datasetName)
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
        [Route("GetPlotsByDatasetName")]
        public async Task<IActionResult> GetPlotsByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.Plots 
                    .Where(w => w.DatasetName == datasetName)
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
        [Route("GetADFTests")]
        public async Task<IActionResult> GetADFTests()
        {
            try
            {
                var query = await _context.ADFTests 
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
        [Route("GetADFTestsById")]
        public async Task<IActionResult> GetADFTestsById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new {Message = "Id null or empty."});
                }
                var query = await _context.ADFTests 
                    .Where(w => w.Id == id)
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
        [Route("GetADFTestsByDatasetName")]
        public async Task<IActionResult> GetADFTestsByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.ADFTests 
                    .Where(w => w.DatasetName == datasetName)
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
        [HttpGet("DeleteADFTestsDataAsync")]
        public async Task<IActionResult> DeleteADFTestsDataAsync(int id)
        {
            
            try
            {
                var query = await _context.ADFTests.FindAsync(id);
                _context.ADFTests.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Record deleted."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetMeanErrors")]
        public async Task<IActionResult> GetMeanErrors()
        {
            try
            {
                var query = await _context.MeanErrors  
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
        [Route("GetMeanErrorsById")]
        public async Task<IActionResult> GetMeanErrorsById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new {Message = "Id null or empty."});
                }
                var query = await _context.MeanErrors  
                    .Where(w => w.Id == id)
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
        [HttpGet("DeleteMeanErrorsDataAsync")]
        public async Task<IActionResult> DeleteMeanErrorsDataAsync(int id)
        {
            
            try
            {
                var query = await _context.MeanErrors.FindAsync(id);
                _context.MeanErrors.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Record deleted."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetMeanErrorsByDatasetName")]
        public async Task<IActionResult> GetMeanErrorsByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.MeanErrors 
                    .Where(w => w.DatasetName == datasetName)
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
        [Route("GetWeatherForecasts")]
        public async Task<IActionResult> GetWeatherForecasts()
        {
            try
            {
                var query = await _context.WeatherForecasts
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
        [Route("GetWeatherForecastsById")]
        public async Task<IActionResult> GetWeatherForecastsById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new {Message = "Id null or empty."});
                }
                var query = await _context.WeatherForecasts
                    .Where(w => w.Id == id)
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
        [HttpGet("DeleteWeatherForecastsAsync")]
        public async Task<IActionResult> DeleteWeatherForecastsAsync(int id)
        {
            
            try
            {
                var query = await _context.WeatherForecasts.FindAsync(id);
                _context.WeatherForecasts.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Record deleted."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetWeatherForecastsByDatasetName")]
        public async Task<IActionResult> GetWeatherForecastsByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.WeatherForecasts 
                    .Where(w => w.DatasetName == datasetName)
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
        [Route("GetWeatherSummaries")]
        public async Task<IActionResult> GetWeatherSummaries()
        {
            try
            {
                var query = await _context.WeatherSummaries
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
        [Route("GetWeatherSummariesById")]
        public async Task<IActionResult> GetWeatherSummariesById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return Ok(new {Message = "Id null or empty."});
                }
                var query = await _context.WeatherSummaries
                    .Where(w => w.Id == id)
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
        [HttpGet("DeleteWeatherSummariesAsync")]
        public async Task<IActionResult> DeleteWeatherSummariesAsync(int id)
        {
            
            try
            {
                var query = await _context.WeatherSummaries.FindAsync(id);
                _context.WeatherSummaries.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Record deleted."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetWeatherSummariesByDatasetName")]
        public async Task<IActionResult> GetWeatherSummariesByDatasetName(string datasetName)
        {
            try
            {
                if (string.IsNullOrEmpty(datasetName))
                {
                    return Ok(new {Message = "Dataset Name null or empty."});
                }
                var query = await _context.WeatherSummaries 
                    .Where(w => w.DatasetName == datasetName)
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
        [HttpPost]
        [Route("PostADFTest")]
        public async Task<IActionResult> PostADFTest([FromBody] ADFTest adfTest) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.ADFTests.Add(adfTest);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostMeanError")]
        public async Task<IActionResult> PostMeanError([FromBody] MeanError meanError) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }
                _context.MeanErrors.Add(meanError);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostWeatherForecast")]
        public async Task<IActionResult> PostWeatherForecast([FromBody] WeatherForecast weatherForecast) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Model not valid"});
                }
                _context.WeatherForecasts.Add(weatherForecast);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostWeatherSummary")]
        public async Task<IActionResult> PostWeatherSummary([FromBody] WeatherSummary weatherSummary) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = "Model not valid"});
                }
                _context.WeatherSummaries.Add(weatherSummary);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetKNNTests")]
        public async Task<IActionResult> GetKNNTests()
        {
            try
            {
                var assets = new {
                    AccuracyPrediction = @"C:\Users\dell\Entrepreneurship\Engineering\Scripts\Data\accuracyPredictionForKNN.txt",
                    ClassificationReport = @"C:\Users\dell\Entrepreneurship\Engineering\Scripts\Data\classificationReportForKNN.txt",
                    MisclassificationErrorRate = 
                        @"C:\Users\dell\Entrepreneurship\Engineering\Scripts\Data\misclassificationErrorRateForKNN.txt",                    
                };
                
                var plots = await _context.Plots
                    .Where(w => w.Id == 14372)
                    .Where(w => w.DatasetName.Contains("classified_data.csv"))
                    .ToListAsync();
                // List<object> accuracyList = new List<object>();

                var accuracyPrediction = System.IO.File.ReadAllLines(assets.AccuracyPrediction);
                var classificationReport = System.IO.File.ReadAllLines(assets.ClassificationReport);
                var classficationErrorRate = System.IO.File.ReadAllLines(assets.MisclassificationErrorRate);

                var indices = new {
                    AccuracyPrediction = accuracyPrediction,
                    ClassificationRep = classificationReport,
                    MisclassificationErrorRate = classficationErrorRate,
                };

                var describe = await _context.Describes
                    .Where(w => w.DatasetName.Contains("classified_data.csv"))
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();

                var data = new {
                    Plots = plots,
                    Indices = indices,
                    Describe = describe,
                };

                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }

        // PlainTextNewsDocument
        [HttpGet]
        [Route("GetNewsInformationNlpLinguisticFeaturesTextFilename")]
        public async Task<IActionResult> GetNewsInformationNlpLinguisticFeaturesTextFilename()
        {
            try
            {
                var query = await _context.PlainTextNewsDocuments
                    .OrderByDescending(o => o.Id)
                    .Select(s => s.Name)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        [Route("GetLinguisticFeatureEndpoints")]
        public async Task<IActionResult> GetLinguisticFeatureEndpoints()
        {
            try
            {
                var query = await _context.LinguisticFeatureEndpoints
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
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpPost]
        [Route("PostLinguisticFeatureEndpoints")]
        public async Task<IActionResult> PostLinguisticFeatureEndpoints([FromBody] LinguisticFeatureEndpoint endpoint)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = $"Error: model state: {ModelState.IsValid}"});
                }
                _context.LinguisticFeatureEndpoints.Add(endpoint);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data save with success status."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        } 
        [HttpGet]
        [Route("GetLinguisticFeaturesEnums")]
        public async Task<IActionResult> GetLinguisticFeaturesEnums()
        {
            try
            {
                var query = await _context.LinguisticFeatureEndpoints
                    .OrderByDescending(o => o.Id)
                    .Select(s => new { s.Feature })
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        [Route("GetLinguisticFeaturesLabels")]
        public async Task<IActionResult> GetLinguisticFeaturesLabels(string filename, string feature)
        {
            try
            {
                switch (feature)
                {
                    case "Entity Recognition":
                        var entities = await _context.Entities
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .GroupBy(g => g.Label)
                            .Select(s => new { Label = s.Key, Count = s.Count()})
                            .OrderBy(o => o.Count)
                            .ToListAsync();
                        if (entities.Count == 0)
                        {
                            return Ok(new { Message = $"Query counts: {entities.Count}" });
                        }
                        return Ok(entities);
                    case "Parse Tree":
                        var parseTrees = await _context.ParseTrees
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .GroupBy(g => new { g.HeadPos, g.Dep })
                            .Select(s => new { HeadPos = s.Key.HeadPos, Dep = s.Key.Dep, Count = s.Count()})
                            .OrderByDescending(o => o.Count)
                            .ToListAsync();
                        if (parseTrees.Count == 0)
                        {
                            return Ok(new { Message = $"Query counts: {parseTrees.Count}" });
                        }
                        return Ok(parseTrees);
                    case "Noun Chunk":
                        var nounChunks = await _context.NounChunks
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .GroupBy(s => s.RootDep)
                            .Select(s => new { RootDep = s.Key, Count = s.Count() })
                            .OrderByDescending(o => o.Count)
                            .ToListAsync();
                        if (nounChunks.Count == 0)
                        {
                            return Ok(new { Message = $"Query counts: {nounChunks.Count}" });
                        }
                        return Ok(nounChunks);
                    case "Part of Speech":
                        var partOfSpeeches = await _context.PartOfSpeech
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .GroupBy(g => new { g.Dep_, g.Pos_ })
                            .Select(s => new { Dep = s.Key.Dep_, Pos_ = s.Key.Pos_, Count = s.Count()})
                            .OrderByDescending(o => o.Count)
                            .ToListAsync();
                        if (partOfSpeeches.Count == 0)
                        {
                            return Ok(new { Message = $"Query counts: {partOfSpeeches.Count}" });
                        }
                        return Ok(partOfSpeeches);
                    default:
                        break;
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        [Route("GetNewsInformationNlpLinguisticFeatures")]
        public async Task<IActionResult> GetNewsInformationNlpLinguisticFeaturesByFileName(
            string filename, string feature, string condition, string token, string label)
        {
            try
            {
                switch (feature)
                {
                    case "Entity Recognition":
                        switch (condition)
                        {
                            case "Count N° rows.":
                                var countRows = await _context.Entities
                                    .Where(w => w.DatasetName.Contains($"{filename}"))
                                    .Select(s => new { Count = s.DatasetName.Count() })
                                    .ToListAsync();
                                if (countRows.Count == 0)
                                {
                                    return Ok(new { Message = $"Query counts: {countRows.Count}" });
                                }
                                return Ok(countRows);
                            case "Count Labels":
                                var countLabels = await _context.Entities
                                    .Where(w => w.DatasetName.Contains($"{filename}"))
                                    .GroupBy(g => g.Label)
                                    .Select(s => new { Label = s.Key, Count = s.Count() })
                                    .ToListAsync();
                                if (countLabels.Count == 0)
                                {
                                    return Ok(new { Message = $"Query counts: {countLabels.Count}" });
                                }
                                return Ok(countLabels);
                            case "Count Token Label Loop":
                                var countTokenLabelLoop = await _context.Entities
                                    .Where(w => w.DatasetName.Contains($"{filename}"))
                                    .Where(w => w.Text.Contains($"{token}"))
                                    .Where(w => w.Label == label)
                                    .GroupBy(g => new {g.Label, g.Text})
                                    .Select(s => new { Label = s.Key.Label, Count = s.Count() })
                                    .ToListAsync();
                                if (countTokenLabelLoop.Count == 0)
                                {
                                    return Ok(new { Message = $"Query counts: {countTokenLabelLoop.Count}" });
                                }
                                return Ok(countTokenLabelLoop);
                            default:
                                break;
                        }                        
                        break;
                    case "Parse Tree":
                        var parseTrees = await _context.ParseTrees
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        break;
                    case "Noun Chunk":
                        var nounChunks = await _context.NounChunks
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        break;
                    case "Part of Speech":
                        var partOfSpeeches = await _context.PartOfSpeech
                            .Where(w => w.DatasetName.Contains($"{filename}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        break;
                    default:
                        break;
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        [Route("GetNounChunks")]
        public async Task<IActionResult> GetNounChunks(string datasetName)
        {
            try
            {
                var query = await _context.NounChunks
                    .Where(w => w.DatasetName.Contains($"{datasetName}"))
                    .OrderByDescending(d => d.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        [Route("GetNewsInformationNlpLinguisticFeatures")]
        public async Task<IActionResult> GetNewsInformationNlpLinguisticFeatures()
        {
            try
            {
                var query = await _context.PlainTextNewsDocuments
                    .OrderByDescending(d => d.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Query counts: {query.Count}" });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpPost]
        [Route("POSTForgeHelp")]
        public async Task<IActionResult> POSTForgeHelp([FromBody] ForgeHelp forgeHelp)
        {
            try
            {
                if (forgeHelp == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.ForgeHelps.Add(forgeHelp);
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
        [Route("GetDatasetsAsync")]
        public async Task<IActionResult> GetDatasetsAsync()
        {
            try
            {
                var datasets = await _context.Datasets.OrderByDescending(d => d.Id).ToListAsync();
                if (datasets.Count == 0)
                {
                    return NoContent();
                }
                return Ok(datasets);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetEndpointsAsync")]
        public async Task<IActionResult> GetEndpointsAsync()
        {
            try
            {
                var data = await _context.EndPoints.OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
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
        [HttpPost]
        [Route("PregnancyOutcomesPOST")]
        public async Task<IActionResult> PregnancyOutcomesPOST([FromBody] PregnancyOutcome pregnancyOutcome)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.PregnancyOutcomes.Add(pregnancyOutcome);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SentimentPlotPOST")]
        public async Task<IActionResult> SentimentPlotPOST([FromBody] SentimentPlot sentimentPlot)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.SentimentPlots.Add(sentimentPlot);
                await _context.SaveChangesAsync();
                return Ok();                
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<IActionResult> GetPlotsByCenturyURL(string century)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(century))
                {
                    return NotFound();   
                }
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
        [HttpPost]
        [Route("POSTYearsDifferences")]
        public async Task<IActionResult> POSTYearsDifferences([FromBody] YearDifference yearDifference)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.YearDifferences.Add(yearDifference);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetMainCenturies")]
        public async Task<IActionResult> GetMainCenturies()
        {
            try
            {
                
                var query = await _context.Entities
                    .Where(e => EF.Functions.Like(e.Text, "[0-9][0-9]__"))
                    .GroupBy(e => e.Text.Substring(0, 2))
                    .Select(g => new
                    {
                        YearCluster = g.Key,
                        COUNTYears = g.Count()
                    })
                    .OrderByDescending(result => result.COUNTYears)
                    .ToListAsync();

                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GroupByCOUNTYears")]
        public async Task<IActionResult> GroupByCOUNTYears(string century)
        {
            try
            {
                var query = await _context.Entities
                    .FromSqlRaw($"SELECT * FROM Entities WHERE Text LIKE '{century}[0-9][0-9]'")
                    .Where(e => e.Text.Length == 4)
                    .GroupBy(e => e.Text)
                    .Select(g => new
                    {
                        Text = g.Key,
                        COUNTYears = g.Count()
                    }).ToListAsync();
                    // .OrderByDescending(e => e.COUNTYears).ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GroupByDateEntities")]
        public async Task<IActionResult> GroupByDateEntities()
        {
            try
            {
                var data = await _context.Entities
                        .Where(e => e.Label == "DATE")
                        .GroupBy(e => new { e.Label, e.Text })
                        .Select(g => new
                        {
                            Label = g.Key.Label,
                            Text = g.Key.Text,
                            COUNTDates = g.Count()
                        })
                        .OrderBy(e => e.COUNTDates).ToListAsync();
                if (data.Count == 0)
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
        [HttpGet]
        [Route("GetSentimentsBySearch")]
        public async Task<IActionResult> GetSentimentsBySearch(string search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search))
                {
                    return NotFound();
                }
                var data = await _context.NLPSentences.Where(d => d.Sentence.Contains($"{search}")).OrderByDescending(d => d.Id).ToListAsync();
                if (data.Count == 0)
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
        [HttpPost]
        [Route("POSTVocabulaireDroit")]
        public async Task<IActionResult> POSTVocabulaireDroit([FromBody] VocabulaireDroit droit)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.VocabulaireDroits.Add(droit);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("POSTDictionnaireManagementProjet")]
        public async Task<IActionResult> POSTDictionnaireManagementProjet([FromBody] DictionnaireManagementProjet projet)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.DictionnaireManagementProjets.Add(projet);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("DeleteLinguisticFeaturesByName")]
        public async Task<IActionResult> DeleteLinguisticFeaturesByName(string filename)
        {
            try
            {
                var getNounChunks = await _context.NounChunks.Where(d => d.DatasetName == filename).ToListAsync();
                _context.NounChunks.RemoveRange(getNounChunks);
                await _context.SaveChangesAsync();

                var getParseTrees = await _context.ParseTrees.Where(d => d.DatasetName == filename).ToListAsync();
                _context.ParseTrees.RemoveRange(getParseTrees);
                await _context.SaveChangesAsync();

                var getLocalTrees = await _context.LocalTrees.Where(d => d.DatasetName == filename).ToListAsync();
                _context.LocalTrees.RemoveRange(getLocalTrees);
                await _context.SaveChangesAsync();

                var getEntities = await _context.Entities.Where(d => d.DatasetName == filename).ToListAsync();
                _context.Entities.RemoveRange(getEntities);
                await _context.SaveChangesAsync();

                var getPos = await _context.PartOfSpeech.Where(d => d.DatasetName == filename).ToListAsync();
                _context.PartOfSpeech.RemoveRange(getPos);
                await _context.SaveChangesAsync();

                return Ok(new {Message = "Data delete successfully, from linguistic features tables."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Address = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetRequests")]
        public async Task<IActionResult> GetRequests()
        {
            var query = await _context.Requests
                .OrderByDescending(o => o.Id)
                .GroupBy(g => new {g.Method, g.Query})
                .Select(
                    s => new {
                        Method = s.Key.Method,
                        Query = s.Key.Query,
                        Count = s.Count()
                    }
                )                
                .ToListAsync();
            if (query.Count == 0)
            {
                return Ok(new { Message = $"Requests entity: {query.Count()} rows." });
            }
            return Ok(new { Message = "Executed requests, by Http methods & endpoints.", Data = query });
        }
        [HttpGet]
        [Route("GetEntityRecognitionTextByLabel")]
        public async Task<IActionResult> GetEntityRecognitionTextByLabel(string filename, string label)
        {
            try
            {
                var query = await _context.Entities
                    .Where(w => w.DatasetName == filename)
                    .Where(w => w.Label == label)
                    .OrderByDescending(o => o.Id)
                    .GroupBy(g => new {
                        g.DatasetName, g.Text
                    })
                    .Select(s => new {
                        DatasetName = s.Key.DatasetName,
                        Text = s.Key.Text,
                        Count = s.Count()
                    })
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Entity Recognition e.g. N° Text: {query.Count()} rows." });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error message, at: GetEntityRecognitionTextByLabel endpoint: {ex.Message}" });
            }
        }
        [HttpGet]
        [Route("GetCountEntityLabels")]
        public async Task<IActionResult> GetCountEntityLabels(string filename)
        {
            try
            {
                var countEntityLabels = await _context.Entities
                    .Where(w => w.DatasetName == filename)
                    .OrderByDescending(d => d.Id)
                    .GroupBy(g => new { g.DatasetName, g.Label })
                    .Select(s => new { 
                        DatasetName = s.Key.DatasetName, 
                        Label = s.Key.Label, 
                        Count = s.Count() 
                    })
                    .Distinct()
                    .ToListAsync();
                if (countEntityLabels.Count == 0)
                {
                    return Ok(new { Message = $"Entity Recognition e.g. N° labels: {countEntityLabels.Count()} rows." });
                }
                return Ok(countEntityLabels);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error message, at: GetEntityRecognitionTextByLabel endpoint: {ex.Message}" });
            }
        }
        [HttpPost]
        [Route("PostCsvEndPoint")]
        public async Task<IActionResult> PostCsvEndPoint([FromBody] WebPage endpoint)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new { Message = $"Model state: {ModelState.IsValid}." });
                }
                _context.WebDocuments.Add(endpoint);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Dataset posted successfully." });
            }            
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("GetCsvEndpoints")]
        public async Task<IActionResult> GetCsvEndpoints()
        {
            try
            {
                var query = await _context.WebDocuments.Select(s => s.Title).Distinct().ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Web Document e.g. N°: {query.Count()} rows." });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("GetCsvEndpointByTitle")]
        public async Task<IActionResult> GetCsvEndpointByTitle(string title)
        {
            try
            {
                var query = await _context.WebDocuments
                    .OrderByDescending(o => o.Id)
                    .Where(w => w.Title == title)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new { Message = $"Web Document {title} e.g. N°: {query.Count()} rows." });
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new { Message = $"Error: {ex.Message}" });
            }
        }
        [HttpGet]
        [Route("GetLinguisticFeaturesParametersEnums")]
        public IActionResult GetLinguisticFeaturesParametersEnums()
        {
            List<object> featuresQueryTypes = new List<object>();
            var fdtData = new {
                Feature = "Token",
                QueryType = "1.Get Token Match"
            };
            featuresQueryTypes.Add(fdtData);
            fdtData = new {
                Feature = "Entity",
                QueryType = "1.Count Entity Labels, 2.Get Entity Recognition Text By Label"
            };
            featuresQueryTypes.Add(fdtData);
            fdtData = new {
                Feature = "LocalTree",
                QueryType = "1.Count Local Tree Dependencies, 2.Get Local Tree Ancestors By Dependency"
            };
            featuresQueryTypes.Add(fdtData);
            fdtData = new {
                Feature = "ParseTree",
                QueryType = "1.Count Parse Tree Dependencies , 2.Get Parse Tree Child By Dependency"
            };
            featuresQueryTypes.Add(fdtData);
            fdtData = new {
                Feature = "PartOfSpeech",
                QueryType = "1.Count Part of Speech Pos , 2.Get Part of Speech Text By Pos"
            };
            featuresQueryTypes.Add(fdtData);
            fdtData = new {
                Feature = "NounChunk",
                QueryType = "1.Count Noun Chunk Dependencies , 2.Get Noun Chunk By Dependency"
            };
            featuresQueryTypes.Add(fdtData);
            
            return Ok(featuresQueryTypes);
        }
        [HttpGet]
        [Route("GetLinguisticFeatures")]
        public async Task<IActionResult> GetLinguisticFeatures(string feature, string queryType, string filename, string label)
        {
            try
            {
                switch (feature)
                {
                    case "Token":
                        switch (queryType)
                        {
                            case "Get Token Match":
                                // var matchLocalTree = await _context.LocalTrees
                                //     .OrderByDescending(o => o.Id)
                                //     .Where(w => w.DatasetName == filename)
                                //     .Where(w => w.Ancestors.Contains($"{label}"))
                                //     .GroupBy(g => new { g.Text, g.Ancestors, g.Dep })
                                //     .Select(s => new 
                                //         { Text = s.Key.Text, Ancestors = s.Key.Ancestors, Dep = s.Key.Dep, Count = s.Count() })
                                //     .ToListAsync();
                                var matchParseTree = await _context.ParseTrees
                                    .OrderByDescending(o => o.Id)
                                    .Where(w => w.Text.Length > 5)
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.Child.Contains($"{label}"))
                                    .GroupBy(s => new { s.Child, s.Text })
                                    .Select(s => new { s.Key.Child, s.Key.Text, Count = s.Count() })
                                    .ToListAsync();
                                var matchNounChunk = await _context.NounChunks
                                    .OrderByDescending(o => o.Id)
                                    .Where(w => w.Text.Length > 5)
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.RootText.Contains($"{label}"))
                                    .GroupBy(s => new { s.Text })
                                    .Select(s => 
                                        new { Text = s.Key.Text, Count = s.Count()})
                                    .ToListAsync();
                                var matchEntityRecognition = await _context.Entities
                                    .OrderByDescending(o => o.Id)
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.Text.Contains($"{label}"))
                                    .GroupBy(s => new { s.Text, s.Label })
                                    .Select(s => new { Text = s.Key.Text, Label = s.Key.Label, Count = s.Count() })
                                    .ToListAsync();

                                var distinctChild = matchParseTree.Select(s => new {
                                        Child = s.Child.Replace('[', ' ').Replace(']', ' ').Split(',').Distinct()
                                    }).Distinct();
                                List<object> listOfChild = new List<object>();
                                foreach (var item in distinctChild)
                                {
                                    foreach (var child in item.Child)
                                    {                  
                                        if (child.Length > 5)
                                        {
                                            
                                            var data = new { Child = child };
                                            listOfChild.Add(data);
                                        }        
                                    }
                                }
                                var getTokenMatch = new {
                                    // LocalTree = matchLocalTree,
                                    ParseTree = matchParseTree
                                        .Where(w => w.Text.Length > 5)
                                        .Select(s => new { s.Text }).Distinct(),
                                    ParseTreeChild = listOfChild.Distinct(),
                                    NounChunk = matchNounChunk,
                                    EntityRecognition = matchEntityRecognition,
                                };

                                return Ok(getTokenMatch);
                            
                            default:
                                break;
                        }
                        break;
                    case "Entity":
                        switch (queryType)
                        {
                            case "Count Entity Labels":
                                var countEntityLabels = await _context.Entities
                                        .Where(w => w.DatasetName == filename)
                                        .OrderByDescending(d => d.Id)
                                        .GroupBy(g => new { g.DatasetName, g.Label })
                                        .Select(s => new { 
                                            DatasetName = s.Key.DatasetName, 
                                            Label = s.Key.Label, 
                                            Count = s.Count() 
                                        })
                                        .Distinct()
                                        .ToListAsync();
                                    if (countEntityLabels.Count == 0)
                                    {
                                        return Ok(new { Message = $"Entity Recognition e.g. N° labels: {countEntityLabels.Count()} rows." });
                                    }
                                    return Ok(countEntityLabels);
                            case "Get Entity Recognition Text By Label":
                                var getEntityRecognitionTextByLabel = await _context.Entities
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.Label == label)
                                    .OrderByDescending(o => o.Id)
                                    .GroupBy(g => new {
                                        g.DatasetName, g.Text
                                    })
                                    .Select(s => new {
                                        DatasetName = s.Key.DatasetName,
                                        Text = s.Key.Text,
                                        Count = s.Count()
                                    })
                                    .ToListAsync();
                                if (getEntityRecognitionTextByLabel.Count == 0)
                                {
                                    return Ok(new { Message = $"Entity Recognition e.g. N° Text: {getEntityRecognitionTextByLabel.Count()} rows." });
                                }
                                return Ok(getEntityRecognitionTextByLabel);
                            default:
                                break;
                        }
                        break;
                    case "LocalTree":
                        switch (queryType)
                        {
                            case "Count Local Tree Dependencies":
                                var countLocalTreeDep = await _context.LocalTrees
                                    .Where(w => w.DatasetName == filename)
                                    .OrderByDescending(d => d.Id)
                                    .GroupBy(g => new { g.DatasetName, g.Dep })
                                    .Select(s => new { 
                                        DatasetName = s.Key.DatasetName, 
                                        Label = s.Key.Dep, 
                                        Count = s.Count() 
                                    })
                                    .Distinct()
                                    .ToListAsync();
                                if (countLocalTreeDep.Count == 0)
                                {
                                    return Ok(new { Message = $"Count Local Tree e.g. N° dependencies: {countLocalTreeDep.Count()} rows." });
                                }
                                return Ok(countLocalTreeDep);
                            case "Get Local Tree Ancestors By Dependency":
                                var getLocalTreeAncestorsByDep = await _context.LocalTrees
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.Dep == label)
                                    .OrderByDescending(o => o.Id)
                                    .GroupBy(g => new {
                                        g.DatasetName, g.Ancestors
                                    })
                                    .Select(s => new {
                                        DatasetName = s.Key.DatasetName,
                                        Text = s.Key.Ancestors,
                                        Count = s.Count()
                                    })
                                    .ToListAsync();
                                if (getLocalTreeAncestorsByDep.Count == 0)
                                {
                                    return Ok(new { Message = $"Local Tree e.g. N° Ancestors: {getLocalTreeAncestorsByDep.Count()} rows." });
                                }
                                return Ok(getLocalTreeAncestorsByDep);
                            default:
                                break;
                        }
                        break;
                    case "ParseTree":
                        switch (queryType)
                        {
                            case "Count Parse Tree Dependencies":
                                var countParseTreeDep = await _context.ParseTrees
                                    .Where(w => w.DatasetName == filename)
                                    .OrderByDescending(d => d.Id)
                                    .GroupBy(g => new { g.DatasetName, g.Dep })
                                    .Select(s => new { 
                                        DatasetName = s.Key.DatasetName, 
                                        Label = s.Key.Dep, 
                                        Count = s.Count() 
                                    })
                                    .Distinct()
                                    .ToListAsync();
                                if (countParseTreeDep.Count == 0)
                                {
                                    return Ok(new { Message = $"Count Parse Tree e.g. N° dependencies: {countParseTreeDep.Count()} rows." });
                                }
                                return Ok(countParseTreeDep);
                            case "Get Parse Tree Child By Dependency":
                                var getParseTreeAncestorsByDep = await _context.ParseTrees
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.Dep == label)
                                    .OrderByDescending(o => o.Id)
                                    .GroupBy(g => new {
                                        g.DatasetName, g.Child
                                    })
                                    .Select(s => new {
                                        DatasetName = s.Key.DatasetName,
                                        Text = s.Key.Child,
                                        Count = s.Count()
                                    })
                                    .ToListAsync();
                                if (getParseTreeAncestorsByDep.Count == 0)
                                {
                                    return Ok(new { Message = $"Parse Tree e.g. N° Child: {getParseTreeAncestorsByDep.Count()} rows." });
                                }
                                return Ok(getParseTreeAncestorsByDep);
                            default:
                                break;
                        }
                        break;
                    case "PartOfSpeech":
                        switch (queryType)
                        {
                            case "Count Part of Speech Pos":
                                var countPartOfSpeech = await _context.PartOfSpeech
                                    .Where(w => w.DatasetName == filename)
                                    .OrderByDescending(d => d.Id)
                                    .GroupBy(g => new { g.DatasetName, g.Pos_ })
                                    .Select(s => new { 
                                        DatasetName = s.Key.DatasetName, 
                                        Label = s.Key.Pos_, 
                                        Count = s.Count() 
                                    })
                                    .Distinct()
                                    .ToListAsync();
                                if (countPartOfSpeech.Count == 0)
                                {
                                    return Ok(new { Message = $"Count Part of Speech e.g. N° Pos: {countPartOfSpeech.Count()} rows." });
                                }
                                return Ok(countPartOfSpeech);
                            case "Get Part of Speech Text By Pos":
                                var getPartOfSpeechByPos = await _context.PartOfSpeech
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.Pos_ == label)
                                    .OrderByDescending(o => o.Id)
                                    .GroupBy(g => new {
                                        g.DatasetName, g.Text_
                                    })
                                    .Select(s => new {
                                        DatasetName = s.Key.DatasetName,
                                        Text = s.Key.Text_,
                                        Count = s.Count()
                                    })
                                    .ToListAsync();
                                if (getPartOfSpeechByPos.Count == 0)
                                {
                                    return Ok(new { Message = $"Parse Tree e.g. N° Child: {getPartOfSpeechByPos.Count()} rows." });
                                }
                                return Ok(getPartOfSpeechByPos);
                            default:
                                break;
                        }
                        break;
                    case "NounChunk":
                        switch (queryType)
                        {
                            case "Count Noun Chunk Dependencies":
                                var countNounChunkDep = await _context.NounChunks
                                    .Where(w => w.DatasetName == filename)
                                    .OrderByDescending(d => d.Id)
                                    .GroupBy(g => new { g.DatasetName, g.RootDep })
                                    .Select(s => new { 
                                        DatasetName = s.Key.DatasetName, 
                                        Label = s.Key.RootDep, 
                                        Count = s.Count() 
                                    })
                                    .Distinct()
                                    .ToListAsync();
                                if (countNounChunkDep.Count == 0)
                                {
                                    return Ok(new { Message = $"Count Parse Tree e.g. N° dependencies: {countNounChunkDep.Count()} rows." });
                                }
                                return Ok(countNounChunkDep);
                            case "Get Noun Chunk By Dependency":
                                var getNounChunkHeadByDep = await _context.NounChunks
                                    .Where(w => w.DatasetName == filename)
                                    .Where(w => w.RootDep == label)
                                    .OrderByDescending(o => o.Id)
                                    .GroupBy(g => new {
                                        g.DatasetName, g.RootDep, g.RootHead
                                    })
                                    .Select(s => new {
                                        DatasetName = s.Key.DatasetName,
                                        RootHead = s.Key.RootHead,
                                        RootDep = s.Key.RootDep,
                                        Count = s.Count()
                                    })
                                    .ToListAsync();
                                if (getNounChunkHeadByDep.Count == 0)
                                {
                                    return Ok(new { Message = $"Noun Chunk e.g. N° Root head: {getNounChunkHeadByDep.Count()} rows." });
                                }
                                return Ok(getNounChunkHeadByDep);
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return Ok(new {Message = $"No Result"});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("POSTEntityRecognition")]
        public async Task<IActionResult> POSTEntityRecognition([FromBody] Entity entity)
        {
            try
            {
                if (entity == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Entities.Add(entity);
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
        [Route("POSTSentiments")]
        public async Task<IActionResult> POSTSentiments([FromBody] NLPSentence sentiments)
        {
            try
            {
                if (sentiments == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.NLPSentences.Add(sentiments);
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
        [Route("POSTParseTree")]
        public async Task<IActionResult> POSTParseTree([FromBody] ParseTree parseTree)
        {
            try
            {
                if (parseTree == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.ParseTrees.Add(parseTree);
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
        [Route("POSTLocalTree")]
        public async Task<IActionResult> POSTLocalTree([FromBody] LocalTree localTree)
        {
            try
            {
                if (localTree == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.LocalTrees.Add(localTree);
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
        [Route("POSTNounChunks")]
        public async Task<IActionResult> POSTNounChunks([FromBody] NounChunk nounChunk)
        {
            try
            {
                if (nounChunk == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.NounChunks.Add(nounChunk);
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
        [Route("POSTPartsOfSpeech")]
        public async Task<IActionResult> POSTPartsOfSpeech([FromBody] Pos partsOfSpeech)
        {
            try
            {
                if (partsOfSpeech == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.PartOfSpeech.Add(partsOfSpeech);
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
        [Route("GetEntities")]
        public async Task<IActionResult> GetEntities(string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(filename) || !filename.Contains(".txt"))
                {
                    return NotFound();
                }
                var entities = await _context.EntityRecognitions.Where(d => d.DatasetName == filename).ToListAsync();
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
        // PartOfSpeechCOUNTs
        [HttpPost]
        [Route("EntityRecognitionPost")]
        public async Task<IActionResult> EntityRecognitionPost([FromBody] EntityRecognition entityRecognition)
        {
            try
            {
                if (entityRecognition == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.EntityRecognitions.Add(entityRecognition);
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
        [Route("ColumnDescriptionByDatasetNames")]
        public async Task<IActionResult> ColumnDescriptionByDatasetNames()
        {
            try
            {                
                var _entityDescriptions = await _context.EntityDescriptions.Where(d => d.DatasetName == "UK_Qualifications.csv").OrderByDescending(d => d.Id).ToListAsync();
                if (_entityDescriptions.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(_entityDescriptions);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("ColumnDescriptionPost")]
        public async Task<IActionResult> ColumnDescriptionPost([FromBody] ColumnDescription columnDescription)
        {
            try
            {
                if (columnDescription == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.EntityDescriptions.Add(columnDescription);
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
        [Route("GetDocumentNames/{top}")]
        public async Task<IActionResult> GetDocumentNames(int top)
        {
            try
            {
                if (top == 0 || string.IsNullOrWhiteSpace(top.ToString()))
                {
                    return NotFound();
                }

                var documents = await _context.Documents
                    .Join(_context.Readings,
                        doc => doc.Id,
                        reading => reading.DocumentID,
                        (doc, reading) => new {
                            DocumentIDMeta = reading.DocumentID.ToString(),
                            DocumentTitle = doc.Title.ToString(),
                            DocumentUrl = doc.Url.ToString(),
                            DocumentCreatedAT = doc.CreatedAT.ToString(),
                            ReadingID = reading.Id.ToString(),                            
                            ReadingDateAT = reading.DocumentID.ToString(),
                            ReadingStartAT = reading.StartAT.ToString(),
                            ReadingEndAT = reading.EndAT.ToString(),
                            ReadCreatedAT = reading.CreatedAT.ToString(),
                        }).Take(top).OrderByDescending(d => d.ReadingID).ToListAsync();
                if (documents.Count == 0)
                {
                    return NoContent();
                }
                return Ok(documents);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PlasmaFrequencies
        [HttpPost]
        [Route("PlasmaFrequenciesPost")]
        public async Task<IActionResult> PlasmaFrequenciesPost([FromBody] PlasmaFrequency plasmaFrequency)
        {
            try
            {
                if (plasmaFrequency == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.PlasmaFrequencies.Add(plasmaFrequency);
                    await _context.SaveChangesAsync();
                }                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SIPrefixesPost")]
        public async Task<IActionResult> SIPrefixesPost([FromBody] SIPrefix siPrefix)
        {
            try
            {
                if (siPrefix == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.SIPrefixes.Add(siPrefix);
                    await _context.SaveChangesAsync();
                }                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("GreekAlphabetsPost")]
        public async Task<IActionResult> GreekAlphabetsPost([FromBody] GreekAlphabet greekAlphabet)
        {
            try
            {
                if (greekAlphabet == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.GreekAlphabets.Add(greekAlphabet);
                    await _context.SaveChangesAsync();
                }                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("AstrophysicalQuantitiesPost")]
        public async Task<IActionResult> AstrophysicalQuantitiesPost([FromBody] AstrophysicalQuantity astrophysicalQuantity)
        {
            try
            {
                if (astrophysicalQuantity == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.AstrophysicalQuantities.Add(astrophysicalQuantity);
                    await _context.SaveChangesAsync();
                }                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("PhysicalConstantsPost")]
        public async Task<IActionResult> PhysicalConstantsPost([FromBody] PhysicalConstant physicalConstant)
        {
            try
            {
                if (physicalConstant == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.PhysicalConstants.Add(physicalConstant);
                    await _context.SaveChangesAsync();
                }                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetPhysicalConstant()
        {
            try
            {
                var data = await _context.PhysicalConstants.OrderBy(d => d.Id).ToListAsync();
                if (data.Count == 0)
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
        
        // Indicator
        [HttpPost]
        [Route("IndicatorsPost")]
        public async Task<IActionResult> IndicatorsPost([FromBody] Indicator indicator)
        {
            try
            {
                if (indicator == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Indicators.Add(indicator);
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
        [Route("RegressionPlotPost")]
        public async Task<IActionResult> RegressionPlotPost([FromBody] RegressionPlot regressionPlot)
        {
            try
            {
                if (regressionPlot == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.RegressionPlots.Add(regressionPlot);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
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
                var infos = await _context.Infos.OrderByDescending(d => d.Id).Where(d => d.DatasetName == datasetName).ToListAsync();
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
        
        [HttpPost]
        [Route("PlotMetadataDetailsPoster")]
        public async Task<IActionResult> PlotMetadataDetailsPoster([FromBody] PlotMetadata plotMetadata)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.PlotMetadataDetails.Add(plotMetadata);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSampleTextFile")]
        public IActionResult GetSampleTextFile(string fileName)
        {
            try
            {
                string fileContent = System.IO.File.ReadAllText(@"" + fileName);
                return Content(fileContent, "text/plain");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("MeanPost")]
        public async Task<IActionResult> MeanPost([FromBody] Mean mean)
        {
            try
            {
                if (mean == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Means.Add(mean);
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
        [Route("InfoPost")]
        public async Task<IActionResult> InfoPost([FromBody] Info info)
        {
            try
            {
                if (info == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Infos.Add(info);
                    await _context.SaveChangesAsync();                        
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetPlotByColumn(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();   
                }
                var data = await _context.PlotByColumns.Where(d => d.Id == id).ToListAsync();
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
        
        public async Task<IActionResult> GetRegressionPlot(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    return NotFound();   
                }
                var data = await _context.Plots.Where(d => d.DatasetName == datasetName).ToListAsync(); 
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
        public async Task<IActionResult> GetPlots(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();   
                }
                var data = await _context.Plots.Where(d => d.Id == id).ToListAsync(); 
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
        [HttpGet("GetInfoDataAsync")]
        public async Task<IActionResult> GetInfoDataAsync(string datasetName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(datasetName))
                {
                    var infos = await _context.Infos.OrderByDescending(d => d.Id).Where(d => d.DatasetName == datasetName).ToListAsync();
                    if (infos.Count() == 0)
                    {
                        return NoContent();
                    }     
                    return Ok(infos);               
                }
                else if (string.IsNullOrWhiteSpace(datasetName))
                {
                    var infos = await _context.Infos.OrderByDescending(d => d.Id).ToListAsync();
                    if (infos.Count() == 0)
                    {
                        return NoContent();
                    }     
                    return Ok(infos);               
                }
                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DeleteInfoDataAsync")]
        public async Task<IActionResult> DeleteInfoDataAsync(int id)
        {
            
            var info = await _context.Infos.FindAsync(id);
            _context.Infos.Remove(info);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("SARAMIXSummaryResultsPoster")]
        public async Task<IActionResult> SARAMIXSummaryResultsPoster([FromBody] SARAMIXSummaryResult saramixSummaryResult)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.SARAMIXSummaryResults.Add(saramixSummaryResult);
                await _context.SaveChangesAsync();
                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("OLSSummaryResultsPoster")]
        public async Task<IActionResult> OLSSummaryResultsPoster([FromBody] OLSSummaryResult olsSummaryResult)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.OLSSummaryResults.Add(olsSummaryResult);
                await _context.SaveChangesAsync();
                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("ClassificationReportPoster")]
        public async Task<IActionResult> ClassificationReportPoster([FromBody] ClassificationReport classificationReport)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.ClassificationReports.Add(classificationReport);
                await _context.SaveChangesAsync();
                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetClassificationReport")]
        public async Task<IActionResult> GetClassificationReport(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName))
                {
                    return NotFound();
                }
                var data = await _context.ClassificationReports.Where(d => d.DatasetName == datasetName).ToListAsync();
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
        [HttpPost]
        [Route("DescribePost")]
        public async Task<IActionResult> DescribePost([FromBody] Describe describe)
        {
            try
            {
                if (describe == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Describes.Add(describe);
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
        [Route("IsNullCountPOST")]
        public async Task<IActionResult> IsNullCountPOST([FromBody] Missing missing)
        {
            try
            {
                if (missing == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Missings.Add(missing);
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
        [Route("EstimationWithConfusionMatrixPOST")]
        public async Task<IActionResult> EstimationWithConfusionMatrixPOST([FromBody] EstimationWithConfusionMatrix estimationWithConfusionMatrix)
        {
            try
            {
                if (estimationWithConfusionMatrix == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.EstimationWithConfusionMatrixs.Add(estimationWithConfusionMatrix);
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
        [Route("GeneralizedLinearModelRegressionResultsPOST")]
        public async Task<IActionResult> GeneralizedLinearModelRegressionResultsPOST([FromBody] GeneralizedLinearModelRegressionResult generalizedLinearModelRegressionResults)
        {
            try
            {
                if (generalizedLinearModelRegressionResults == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.GeneralizedLinearModelRegressionResults.Add(generalizedLinearModelRegressionResults);
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
        [Route("RegressionResultPOST")]
        public async Task<IActionResult> RegressionResultPOST([FromBody] RegressionResult regressionResult)
        {
            try
            {
                if (regressionResult == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.RegressionResults.Add(regressionResult);
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
        [Route("PredictProbabilityDecisionPOST")]
        public async Task<IActionResult> PredictProbabilityDecisionPOST([FromBody] PredictProbabilityDecision predictProbabilityDecision)
        {
            try
            {
                if (predictProbabilityDecision == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.PredictProbabilityDecisions.Add(predictProbabilityDecision);
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
        [Route("InterceptPOST")]
        public async Task<IActionResult> InterceptPOST([FromBody] Intercept intercept)
        {
            try
            {
                if (intercept == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Intercepts.Add(intercept);
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
        [Route("ScorePOST")]
        public async Task<IActionResult> ScorePOST([FromBody] Score score)
        {
            try
            {
                if (score == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Scores.Add(score);
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
        [Route("ArrayCoefficientPOST")]
        public async Task<IActionResult> ArrayCoefficientPOST([FromBody] ArrayCoefficient arrayCoefficient)
        {
            try
            {
                if (arrayCoefficient == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.ArrayCoefficients.Add(arrayCoefficient);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetShapeByDatasetName")]
        public async Task<IActionResult> GetShapeByDatasetName(string datasetName)
        {
            try
            {
                var shapes = await _context.Shapes
                    .Where(w => w.DatasetName == datasetName)
                    .OrderByDescending(d => d.Id).ToListAsync();
                if (shapes.Count() == 0)
                {
                    return NoContent();
                }     
                return Ok(shapes);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("ShapePOST")]
        public async Task<IActionResult> ShapePOST([FromBody] Shape shape)
        {
            try
            {
                if (shape == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Shapes.Add(shape);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetDescribeByDatasetAsync")]
        public async Task<IActionResult> GetDescribeByDatasetAsync(string datasetName)
        {
            try
            {
                var describes = await _context.Describes
                    .Where(w => w.DatasetName == datasetName)
                    .OrderByDescending(d => d.Id).ToListAsync();
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
        [HttpGet("GetDescribeDataAsync")]
        public async Task<IActionResult> GetDescribeDataAsync()
        {
            try
            {
                var describes = await _context.Describes.OrderByDescending(d => d.Id).ToListAsync();
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
        [HttpGet("DeleteDescribeDataAsync")]
        public async Task<IActionResult> DeleteDescribeDataAsync(int id)
        {
            
            var describe = await _context.Describes.FindAsync(id);
            _context.Describes.Remove(describe);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost]
        [Route("ColumnsPost")]
        public async Task<IActionResult> ColumnsPost([FromBody] Columns columns)
        {
            try
            {
                if (columns == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Columns.Add(columns);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetColumnsByDatasetNAME")]
        public async Task<IActionResult> GetColumnsByDatasetNAME(string datasetName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(datasetName) || !System.IO.File.Exists(datasetName) || !datasetName.Contains(".csv"))
                {
                    return NotFound();
                }
                var columns = await _context.Columns.Where(d => d.DatasetName == Path.GetFileName(datasetName)).OrderByDescending(d => d.Id).ToListAsync();
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
        [HttpGet("GetFromDbColumnsByDatasetName")]
        public async Task<IActionResult> GetFromDbColumnsByDatasetName(string datasetName)
        {
            try
            {
                var columns = await _context.Columns
                    .Where(d => d.DatasetName == datasetName)
                    .OrderByDescending(d => d.Id)
                    .ToListAsync();
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
        [HttpGet("GetColumnsDataAsync")]
        public async Task<IActionResult> GetColumnsDataAsync()
        {
            try
            {
                var columns = await _context.Columns.OrderByDescending(d => d.Id).ToListAsync();
                if (columns.Count() == 0)
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
        [HttpPost]
        [Route("PlotPost")]
        public async Task<IActionResult> PlotPost([FromBody] Plot plot)
        {
            try
            {
                if (plot == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Plots.Add(plot);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPlotsDataAsync")]
        public async Task<IActionResult> GetPlotsDataAsync()
        {
            try
            {
                var plots = await _context.Plots.OrderByDescending(d => d.Id).ToListAsync();
                if (plots.Count() == 0)
                {
                    return NoContent();
                }     
                return Ok(plots);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPlotDataByIdAsync")]
        public async Task<IActionResult> GetPlotDataByIdAsync(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();
                }
                var plots = await _context.Plots.FindAsync(id);
                
                return Ok(plots);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DeletePlotDataAsync")]
        public async Task<IActionResult> DeletePlotDataAsync(int id)
        {
            
            try
            {
                var plot = await _context.Plots.FindAsync(id);
                _context.Plots.Remove(plot);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Record deleted."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PlotByColumnPost")]
        public async Task<IActionResult> PlotByColumnPost([FromBody] PlotByColumn plotByColumn)
        {
            try
            {
                if (plotByColumn == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.PlotByColumns.Add(plotByColumn);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPlotByColumnsDataAsync")]
        public async Task<IActionResult> GetPlotByColumnsDataAsync()
        {
            try
            {
                var plotByColumns = await _context.PlotByColumns.OrderByDescending(d => d.Id).ToListAsync();
                if (plotByColumns.Count() == 0)
                {
                    return NoContent();
                }     
                return Ok(plotByColumns);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPlotByColumnDataByIdAsync")]
        public async Task<IActionResult> GetPlotByColumnDataByIdAsync(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();
                }
                var plots = await _context.PlotByColumns.FindAsync(id);
                
                return Ok(plots);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DeletePlotByColumnDataAsync")]
        public async Task<IActionResult> DeletePlotByColumnDataAsync(int id)
        {
            
            var plot = await _context.PlotByColumns.FindAsync(id);
            _context.PlotByColumns.Remove(plot);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("CorrelationPost")]
        public async Task<IActionResult> CorrelationPost([FromBody] Correlation correlation)
        {
            try
            {
                if (correlation == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Correlations.Add(correlation);
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
        [Route("WeatherCorrelationPost")]
        public async Task<IActionResult> WeatherCorrelationPost([FromBody] WeatherCorrelation correlation)
        {
            try
            {
                if (correlation == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.WeatherCorrelations.Add(correlation);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetWeatherCorrelationDataAsync")]
        public async Task<IActionResult> GetWeatherCorrelationDataAsync(string datasetName)
        {
            try
            {
                var correlations = await _context.WeatherCorrelations
                    .Where(w => w.DatasetName == datasetName)
                    .OrderByDescending(d => d.Id).ToListAsync();
                if (correlations.Count() == 0)
                {
                    return NoContent();
                }     
                return Ok(correlations);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCorrelationDataAsync")]
        public async Task<IActionResult> GetCorrelationDataAsync()
        {
            try
            {
                var correlations = await _context.Correlations.OrderByDescending(d => d.Id).ToListAsync();
                if (correlations.Count() == 0)
                {
                    return NoContent();
                }     
                return Ok(correlations);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCorrelationDataByIdAsync")]
        public async Task<IActionResult> GetCorrelationDataByIdAsync(int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return NotFound();
                }
                var correlation = await _context.Correlations.FindAsync(id);
                
                return Ok(correlation);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DeleteCorrelationDataAsync")]
        public async Task<IActionResult> DeleteCorrelationDataAsync(int id)
        {
            
            var correlation = await _context.Correlations.FindAsync(id);
            _context.Correlations.Remove(correlation);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("DeleteWeatherCorrelationDataAsync")]
        public async Task<IActionResult> DeleteWeatherCorrelationDataAsync(string datasetName)
        {
            
            var correlation = await _context.WeatherCorrelations
                .Where(w => w.DatasetName == datasetName)
                .ToListAsync();
            _context.WeatherCorrelations.RemoveRange(correlation);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("CoefficientPost")]
        public async Task<IActionResult> CoefficientPost([FromBody] Coefficient coefficient)
        {
            try
            {
                if (coefficient == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Coefficients.Add(coefficient);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}