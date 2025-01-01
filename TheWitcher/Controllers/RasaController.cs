using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWitcher.Data;
using TheWitcher.Data.Data;
using TheWitcher.Data.Models;

namespace TheWitcher.Controllers
{
    public class RasaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RasaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rasa

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rasy = await _context.Rasy.ToListAsync();
            return View(rasy);
        }

        // GET: Rasa/Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rasa/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rasa rasa)
        {
            if (ModelState.IsValid)
            {
                _context.Rasy.Add(rasa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rasa);
        }

        // GET: Rasa/Edit/{id}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rasa = await _context.Rasy.FirstOrDefaultAsync(r => r.Id == id);
            if (rasa == null)
            {
                return NotFound();
            }
            return View(rasa);
        }

        // POST: Rasa/Edit/{id}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Rasa rasa)
        {
            if (ModelState.IsValid)
            {
                _context.Rasy.Update(rasa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rasa);
        }

        // GET: Rasa/Delete/{id}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var rasa = await _context.Rasy.FirstOrDefaultAsync(r => r.Id == id);
            if (rasa == null)
            {
                return NotFound();
            }
            return View(rasa);
        }

        // POST: Rasa/Delete/{id}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rasa = await _context.Rasy.FirstOrDefaultAsync(r => r.Id == id);
            if (rasa != null)
            {
                _context.Rasy.Remove(rasa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
