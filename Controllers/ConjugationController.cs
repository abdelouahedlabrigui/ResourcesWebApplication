using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Tenses;

namespace ResourcesWebApplication.Controllers.English
{
    public class ConjugationController : Controller
    {
        public readonly ApplicationDbContext _context;
        public ConjugationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Conjugation>> GetConjugations()
        {
            try
            {
                var conjugations = _context.Conjugations.OrderByDescending(d => d.Id).ToList();
                return Ok(conjugations);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddConjugation([FromBody] Conjugation conjugation)
        {
            try
            {
                if (conjugation == null)
                {
                    return BadRequest("Object null.");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object.");
                }
                else{
                    _context.Conjugations.Add(conjugation);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllConjugations()
        {
            try
            {
                _context.Conjugations.RemoveRange();
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}