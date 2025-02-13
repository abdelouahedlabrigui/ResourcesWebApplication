using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Careers;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.GenerativeAI;

namespace ResourcesWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CareersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Get200OKSatusTest")]
        public IActionResult Get200OKSatusTest()
        {
            return Ok();
        }
        [HttpGet]
        [Route("GetCodeGenInterpretions")]
        public async Task<IActionResult> GetCodeGenInterpretions()
        {
            try
            {
                var query = await _context.CodeGenInterpretions
                    .Where(w => w.Id >= 10002)
                    .Where(w => w.Id <= 10023)
                    .Select(s => new CodeGenInterpretion {
                        Id = s.Id,
                        LlmAgent = s.LlmAgent,
                        CodeLine = s.CodeLine
                        .Replace(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\codes\codes\", "")
                        .Replace(".txt", ""),
                        Interpretation = s.Interpretation
                        .Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.\n ", ""),
                        CreatedAT = s.CreatedAT
                    })
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query Count {query.Count}" });
                }
                return Ok(query);                                    
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var query = await _context.Roles
                    .ToListAsync();
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("GetRoleDetails")]
        public async Task<IActionResult> GetRoleDetails()
        {
            try
            {
                var query = await _context.RoleDetails
                    .Select(s => new RoleDetail {
                        Id = s.Id,
                        Title = s.Title,
                        TitleDetail = s.TitleDetail.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace("\n", " "),
                        Skill = s.Skill,
                        SkillDetail = s.SkillDetail.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace("\n", " "),
                        Detail = s.Detail,
                        DetailDepth = s.DetailDepth.Replace("failed to get console mode for stdout: The handle is invalid.\nfailed to get console mode for stderr: The handle is invalid.", "").Replace("\n", " "),
                    })
                    .ToListAsync();
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        
        [HttpPost]
        [Route("PostRole")]
        public async Task<IActionResult> PostRole([FromBody] Role role) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }                
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("PostRoleDetail")]
        public async Task<IActionResult> PostRoleDetail([FromBody] RoleDetail roleDetail) {
            try
            {
                if (!ModelState.IsValid)
                {
                    // return Ok(new {Message = "Model not valid"});
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new {Message = "Model not valid", Errors = errors});
                }                
                _context.RoleDetails.Add(roleDetail);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Data saved to database."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = ex.Message});
            }
        }
    }
}