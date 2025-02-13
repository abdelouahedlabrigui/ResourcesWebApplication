using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Knowledge;

namespace ResourcesWebApplication.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.OrderByDescending(d => d.Id).ToListAsync());
        }

        // GET: Cases
        public async Task<IActionResult> CasesIndex()
        {
            return View(await _context.Cases.OrderByDescending(d => d.Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SearchBySkill(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return NotFound();
            }
            var skills = await _context.Skills
                .Where(d => d.Title.Contains($"{title}")).OrderByDescending(d => d.Id).ToListAsync();
            
            return View(skills);
        }
        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            
            var documents = await _context.Documents.Distinct().OrderByDescending(d => d.Id).ToListAsync();
            List<int> documentIds = new List<int>();
            foreach (var item in documents)
            {
                documentIds.Add(item.Id);
            }

            var codes = await _context.Codes.Distinct().OrderByDescending(d => d.Id).ToListAsync();
            List<int> codeIds = new List<int>();
            foreach (var item in codes)
            {
                codeIds.Add(item.Id);                
            }
            Mixed mixed = new Mixed()
            {
                Skill = new Skill()
                {
                    Id = skill.Id,
                    Category = skill.Category,
                    Title = skill.Title,
                    Detail = skill.Detail
                },
                DocumentIDs = documentIds,
                CodeID = codeIds
            };
            if (skill == null)
            {
                return NotFound();
            }

            return View(mixed);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Title,Detail")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }
        [HttpPost]
        public async Task<IActionResult> AddCase([FromBody] List<Case> cases)
        {
            try
            {
                foreach (var item in cases)
                {
                    Case data = new Case()
                    {
                        SkillID = item.SkillID,
                        DocumentID = item.DocumentID,
                        CodeID = item.CodeID,
                        Title = item.Title,
                        Description = item.Description,
                        CreatedAT = item.CreatedAT
                    };
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                }
                return Ok(new {Message = "Data added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Title,Detail")] Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
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
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
