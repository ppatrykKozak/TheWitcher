using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheWitcher.Data;
using TheWitcher.Data.Data;
using TheWitcher.Data.Models;

namespace TheWitcher.Controllers
{
    public class PostacController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera z wstrzyknięciem ApplicationDbContext
        public PostacController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja Index - Pobiera wszystkie postacie z bazy
        public async Task<IActionResult> Index()
        {
            var postacie = await _context.Postacie.ToListAsync();
            ViewBag.Rasy = await _context.Rasy.ToDictionaryAsync(r => r.Id, r => r.Nazwa);
            return View(postacie);
        }

        // Akcja Create - GET
        public async Task<IActionResult> Create()
        {
            ViewBag.Rasy = await _context.Rasy.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nazwa
            }).ToListAsync();

            return View();
        }

        // Akcja Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Postac postac)
        {


            if (ModelState.IsValid)
            {
                _context.Postacie.Add(postac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Rasy = await _context.Rasy.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nazwa
            }).ToListAsync();

            return View(postac);
        }

        // Akcja Details - Pobiera szczegóły postaci z bazy
        public async Task<IActionResult> Details(int id)
        {
            var postac = await _context.Postacie.Include(p => p.Ekwipunek).FirstOrDefaultAsync(p => p.Id == id);
            if (postac == null)
            {
                return NotFound();
            }

            var rasa = await _context.Rasy.FirstOrDefaultAsync(r => r.Id == postac.RasaId);
            ViewBag.RasaNazwa = rasa != null ? rasa.Nazwa : "Nieznana";

            return View(postac);
        }

        // Akcja Edit - GET
        public async Task<IActionResult> Edit(int id)
        {
            var postac = await _context.Postacie.FirstOrDefaultAsync(p => p.Id == id);
            if (postac == null)
            {
                return NotFound();
            }

            ViewBag.Rasy = await _context.Rasy.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nazwa
            }).ToListAsync();

            return View(postac);
        }

        // Akcja Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Postac postac)
        {
          

            if (ModelState.IsValid)
            {
                _context.Postacie.Update(postac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Rasy = await _context.Rasy.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nazwa
            }).ToListAsync();

            return View(postac);
        }

        // Akcja Delete - GET
        public async Task<IActionResult> Delete(int id)
        {
            var postac = await _context.Postacie.FirstOrDefaultAsync(p => p.Id == id);
            if (postac == null)
            {
                return NotFound();
            }

            return View(postac);
        }

        // Akcja Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postac = await _context.Postacie.FirstOrDefaultAsync(p => p.Id == id);
            if (postac != null)
            {
                _context.Postacie.Remove(postac);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
