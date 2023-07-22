using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromptShare.Data;
using PromptShare.Models;

namespace PromptShare.Controllers
{
    public class PromptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prompts
        public async Task<IActionResult> Index()
        {
              return _context.Prompt != null ? 
                          View(await _context.Prompt.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Prompt'  is null.");
        }


        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",await _context.Prompt.Where( j=> j.PromptHead.Contains(SearchPhrase) ).ToListAsync());
        }

        // GET: Prompts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prompt == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prompt == null)
            {
                return NotFound();
            }

            return View(prompt);
        }

        // GET: Prompts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prompts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PromptHead,PromptDes")] Prompt prompt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prompt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prompt);
        }

        // GET: Prompts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prompt == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompt.FindAsync(id);
            if (prompt == null)
            {
                return NotFound();
            }
            return View(prompt);
        }

        // POST: Prompts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PromptHead,PromptDes")] Prompt prompt)
        {
            if (id != prompt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prompt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromptExists(prompt.Id))
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
            return View(prompt);
        }

        // GET: Prompts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prompt == null)
            {
                return NotFound();
            }

            var prompt = await _context.Prompt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prompt == null)
            {
                return NotFound();
            }

            return View(prompt);
        }

        // POST: Prompts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prompt == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Prompt'  is null.");
            }
            var prompt = await _context.Prompt.FindAsync(id);
            if (prompt != null)
            {
                _context.Prompt.Remove(prompt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromptExists(int id)
        {
          return (_context.Prompt?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
