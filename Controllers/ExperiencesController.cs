using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Resume;

namespace ResourcesWebApplication.Controllers
{
    // public class ExperiencesController : Controller
    // {
    //     private readonly ApplicationDbContext _context;

    //     public ExperiencesController(ApplicationDbContext context)
    //     {
    //         _context = context;
    //     }

    //     // GET: Experiences
    //     public async Task<IActionResult> Index()
    //     {
    //         return View(await _context.Experiences.ToListAsync());
    //     }

    //     // GET: Experiences/Details/5
    //     public async Task<IActionResult> Details(int? id)
    //     {
    //         if (id == null)
    //         {
    //             return NotFound();
    //         }

    //         var experience = await _context.Experiences
    //             .FirstOrDefaultAsync(m => m.Id == id);
    //         if (experience == null)
    //         {
    //             return NotFound();
    //         }

    //         return View(experience);
    //     }

    //     // GET: Experiences/Create
    //     public IActionResult Create()
    //     {
    //         return View();
    //     }

    //     // POST: Experiences/Create
    //     // To protect from overposting attacks, enable the specific properties you want to bind to.
    //     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //     [HttpPost]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> Create([Bind("Id,Title,Company,StartDate,EndDate,Description,CreatedAT")] Experience experience)
    //     {
    //         if (ModelState.IsValid)
    //         {
    //             _context.Add(experience);
    //             await _context.SaveChangesAsync();
    //             return RedirectToAction(nameof(Index));
    //         }
    //         return View(experience);
    //     }

    //     // GET: Experiences/Edit/5
    //     public async Task<IActionResult> Edit(int? id)
    //     {
    //         if (id == null)
    //         {
    //             return NotFound();
    //         }

    //         var experience = await _context.Experiences.FindAsync(id);
    //         if (experience == null)
    //         {
    //             return NotFound();
    //         }
    //         return View(experience);
    //     }

    //     // POST: Experiences/Edit/5
    //     // To protect from overposting attacks, enable the specific properties you want to bind to.
    //     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //     [HttpPost]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Company,StartDate,EndDate,Description,CreatedAT")] Experience experience)
    //     {
    //         if (id != experience.Id)
    //         {
    //             return NotFound();
    //         }

    //         if (ModelState.IsValid)
    //         {
    //             try
    //             {
    //                 _context.Update(experience);
    //                 await _context.SaveChangesAsync();
    //             }
    //             catch (DbUpdateConcurrencyException)
    //             {
    //                 if (!ExperienceExists(experience.Id))
    //                 {
    //                     return NotFound();
    //                 }
    //                 else
    //                 {
    //                     throw;
    //                 }
    //             }
    //             return RedirectToAction(nameof(Index));
    //         }
    //         return View(experience);
    //     }

    //     // GET: Experiences/Delete/5
    //     public async Task<IActionResult> Delete(int? id)
    //     {
    //         if (id == null)
    //         {
    //             return NotFound();
    //         }

    //         var experience = await _context.Experiences
    //             .FirstOrDefaultAsync(m => m.Id == id);
    //         if (experience == null)
    //         {
    //             return NotFound();
    //         }

    //         return View(experience);
    //     }

    //     // POST: Experiences/Delete/5
    //     [HttpPost, ActionName("Delete")]
    //     [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> DeleteConfirmed(int id)
    //     {
    //         var experience = await _context.Experiences.FindAsync(id);
    //         _context.Experiences.Remove(experience);
    //         await _context.SaveChangesAsync();
    //         return RedirectToAction(nameof(Index));
    //     }

    //     private bool ExperienceExists(int id)
    //     {
    //         return _context.Experiences.Any(e => e.Id == id);
    //     }
    // }
}
