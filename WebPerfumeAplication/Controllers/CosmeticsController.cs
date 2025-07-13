using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCosmeticApp.Data;
using WebCosmeticApp.Models;

namespace WebCosmeticApp.Controllers
{
    public class CosmeticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CosmeticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cosmetics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cosmetics.ToListAsync());
        }

        // GET: Cosmetics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _context.Cosmetics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cosmetic == null)
            {
                return NotFound();
            }

            return View(cosmetic);
        }

        // GET: Cosmetics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cosmetics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Category,Picture,Description,Price,Quantity")] Cosmetic cosmetic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosmetic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cosmetic);
        }

        // GET: Cosmetics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _context.Cosmetics.FindAsync(id);
            if (cosmetic == null)
            {
                return NotFound();
            }
            return View(cosmetic);
        }

        // POST: Cosmetics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Category,Picture,Description,Price,Quantity")] Cosmetic cosmetic)
        {
            if (id != cosmetic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosmetic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosmeticExists(cosmetic.Id))
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
            return View(cosmetic);
        }

        // GET: Cosmetics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosmetic = await _context.Cosmetics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cosmetic == null)
            {
                return NotFound();
            }

            return View(cosmetic);
        }

        // POST: Cosmetics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cosmetic = await _context.Cosmetics.FindAsync(id);
            if (cosmetic != null)
            {
                _context.Cosmetics.Remove(cosmetic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosmeticExists(int id)
        {
            return _context.Cosmetics.Any(e => e.Id == id);
        }
    }
}
