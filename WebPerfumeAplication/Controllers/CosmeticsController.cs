using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCosmeticApp.Data;
using WebCosmeticApp.Models;

namespace WebCosmeticApp.Controllers
{
    public class CosmeticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CosmeticsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
                return NotFound();

            var cosmetic = await _context.Cosmetics.FirstOrDefaultAsync(m => m.Id == id);
            if (cosmetic == null)
                return NotFound();

            return View(cosmetic);
        }

        // GET: Cosmetics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cosmetics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cosmetic cosmetic)
        {
            if (ModelState.IsValid)
            {
                if (cosmetic.ImageFile != null)
                {
                    var wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(cosmetic.ImageFile.FileName);
                    string extension = Path.GetExtension(cosmetic.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/uploads/", fileName);

                    // Save file
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cosmetic.ImageFile.CopyToAsync(fileStream);
                    }

                    // Save file name in DB
                    cosmetic.Picture = fileName;
                }

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
                return NotFound();

            var cosmetic = await _context.Cosmetics.FindAsync(id);
            if (cosmetic == null)
                return NotFound();

            return View(cosmetic);
        }

        // POST: Cosmetics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cosmetic cosmetic)
        {
            if (id != cosmetic.Id)
                return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cosmetic);
        }

        // GET: Cosmetics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cosmetic = await _context.Cosmetics.FirstOrDefaultAsync(m => m.Id == id);
            if (cosmetic == null)
                return NotFound();

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
                // Delete image from wwwroot/uploads
                if (!string.IsNullOrEmpty(cosmetic.Picture))
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", cosmetic.Picture);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Cosmetics.Remove(cosmetic);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CosmeticExists(int id)
        {
            return _context.Cosmetics.Any(e => e.Id == id);
        }
    }
}
