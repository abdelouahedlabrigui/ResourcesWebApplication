using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Politics;

namespace ResourcesWebApplication.Controllers.Politics
{
    [Route("[controller]")]
    public class PresidentsController : Controller
    {

        public readonly ApplicationDbContext _dbContext;
        public PresidentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        [Route("PopulateTable")]
        public async Task<IActionResult> PopulateTable([FromBody] USPresident presidents)
        {
            try
            {
                if (presidents == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _dbContext.USPresidents.Add(presidents);
                    await _dbContext.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("PopulateVicePresidentTable")]
        public async Task<IActionResult> PopulateVicePresidentTable([FromBody] USVicePresident vicePresident)
        {
            try
            {
                if (vicePresident == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _dbContext.USVicePresidents.Add(vicePresident);
                    await _dbContext.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetDataAsync")]
        public async Task<IActionResult> GetDataAsync()
        {
            try
            {
                var presidents = await _dbContext.USPresidents.OrderByDescending(d => d.Id).ToListAsync();
                if (presidents.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(presidents);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetVicePresidentDataAsync")]
        public async Task<IActionResult> GetVicePresidentDataAsync()
        {
            try
            {
                var presidents = await _dbContext.USVicePresidents.OrderByDescending(d => d.Id).ToListAsync();
                if (presidents.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(presidents);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetDataByVicePresidentName")]
        public async Task<IActionResult> GetDataByVicePresidentName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return NoContent();
                }
                var vicePresident = await _dbContext.USVicePresidents.Where(d => d.VicePresidentOfTheUnitedStates == name).ToListAsync();
                if (vicePresident.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(vicePresident);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}