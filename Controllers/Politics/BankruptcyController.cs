using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Politics.Law;

namespace ResourcesWebApplication.Controllers.Politics
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class BankruptcyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankruptcyController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // [Route("GetProfessionTerms")]
        public async Task<IActionResult> GetProfessionTerms()
        {
            try
            {
                var query = await _context.Professions.OrderByDescending(d => d.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        // [Route("PostProfessionTerms")]
        public async Task<IActionResult> PostProfessionTerms([FromBody] Profession profession)
        {
            try
            {
                if (profession == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Professions.Add(profession);
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