using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Aerospace;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers.Aerospace
{
    [Route("[controller]")]
    public class AircraftStructuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AircraftStructuresController(ApplicationDbContext context)
        {
            _context = context;
        }        
        [HttpGet]
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
        public async Task<IActionResult> GetTopDescConceptDefinition()
        {
            var concept = await _context.AircraftStructuresConceptDefinitions
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
    }
}