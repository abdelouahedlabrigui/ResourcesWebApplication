using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Forensics;

namespace ResourcesWebApplication.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetArticleById(int id)
        {
            try
            {
                var data = await _context.Articles.OrderByDescending(d => d.Id).Where(d => d.Id == id).ToListAsync();
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
        public async Task<IActionResult> GetDataAddressById(int id)
        {
            try
            {
                var data = await _context.DataAddresses.OrderByDescending(d => d.Id).Where(d => d.Id == id).ToListAsync();
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

        public async Task<IActionResult> GetDataAddressIds()
        {
            try
            {
                var data = await _context.DataAddresses.OrderByDescending(d => d.Id).Select(d => d.Id).ToListAsync();
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

        public async Task<IActionResult> GetArticlesIds()
        {
            try
            {
                var data = await _context.Articles.OrderByDescending(d => d.Id).Select(d => d.Id).ToListAsync();
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
        public async Task<IActionResult> DataAddressPoster(int articleId, string name, string url, string createdAT)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articleId.ToString()) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound();
                }
                Data dataAddress = new Data(){
                    ArticleID = articleId,
                    Name = name,
                    Url = url,
                    CreatedAT = createdAT
                };
                _context.DataAddresses.Add(dataAddress);
                await _context.SaveChangesAsync();
                await Task.Delay(4);
                var data = await _context.DataAddresses.Take(4).OrderByDescending(d => d.Id).ToListAsync();
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
        public async Task<IActionResult> ArticlePoster(string title, string url, string createdAT)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound();
                }
                Article article = new Article(){
                    Title = title,
                    Url = url,
                    CreatedAT = createdAT
                };
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                await Task.Delay(4);
                var data = await _context.Articles.Take(4).OrderByDescending(d => d.Id).ToListAsync();
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

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.Take(9).OrderByDescending(d => d.Id).ToListAsync());
        }
        public async Task<IActionResult> IndexDataAddress()
        {
            return View(await _context.DataAddresses.Take(9).OrderByDescending(d => d.Id).ToListAsync());
        }
        public async Task<IActionResult> ArticleIndex()
        {
            return Ok(await _context.Articles.Take(4).OrderByDescending(d => d.Id).ToListAsync());
        }
        public async Task<IActionResult> DataIndex()
        {
            return Ok(await _context.DataAddresses.Take(4).OrderByDescending(d => d.Id).ToListAsync());
        }
        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
        public async Task<IActionResult> DetailsDataAddress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.DataAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,CreatedAT")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        public async Task<IActionResult> EditDataAddress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.DataAddresses.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,CreatedAT")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            return View(article);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDataAddress(int id, [Bind("Id,ArticleID,Name,Url,CreatedAT")] Data data)
        {
            if (id != data.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataAddressExists(data.Id))
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
            return View(data);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            } 
            
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Articles/Delete/5
        public async Task<IActionResult> DeleteDataAddress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.DataAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("DeleteDataAddress")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDataAddressConfirmed(int id)
        {
            var data = await _context.DataAddresses.FindAsync(id);
            _context.DataAddresses.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
        private bool DataAddressExists(int id)
        {
            return _context.DataAddresses.Any(e => e.Id == id);
        }
    }
}
