using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWitcher.Data;
using TheWitcher.Data.Data;
using TheWitcher.Data.Models;

namespace TheWitcher.Controllers
{
    public class EkwipunekController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EkwipunekController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int postacId)
        {
            var postac = await _context.Postacie.Include(p => p.Ekwipunek)
                                                .FirstOrDefaultAsync(p => p.Id == postacId);

            if (postac == null)
            {
                return NotFound("Nie znaleziono postaci.");
            }

            ViewBag.PostacId = postacId;
            ViewBag.PostacName = postac.Imie;
            return View(postac.Ekwipunek); // Zwraca ekwipunek do widoku
        }

        // Create - GET


        [HttpGet]
        public IActionResult Create(int postacId)
        {
            ViewBag.PostacId = postacId;
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postacId, string nazwa, string typ)
        {

           

            var postac = await _context.Postacie.Include(p => p.Ekwipunek)
                                                .FirstOrDefaultAsync(p => p.Id == postacId);
            
            if (postac == null)
            {
                return NotFound("Nie znaleziono postaci.");
            }

            var ekwipunek = new Ekwipunek
            {
                Nazwa = nazwa,
                Typ = typ,
                PostacId = postacId
            };

            _context.Ekwipunki.Add(ekwipunek);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Postac", new { id = postacId });  // Po dodaniu ekwipunku wracamy do szczegółów postaci
        }


        //Edit Get


        [HttpGet]
        public async Task<IActionResult> Edit(int id, int postacId)
        {
            var ekwipunek = await _context.Ekwipunki.FirstOrDefaultAsync(e => e.Id == id && e.PostacId == postacId);

            if (ekwipunek == null)
            {
                return NotFound("Nie znaleziono ekwipunku.");
            }

            ViewBag.PostacId = postacId;
            return View(ekwipunek);
        }

        //Edit Post


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ekwipunek ekwipunek)
        {

            if (ModelState.IsValid)
            {
                _context.Ekwipunki.Update(ekwipunek);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Postac", new { id = ekwipunek.PostacId });
            }

            ViewBag.PostacId = ekwipunek.PostacId;
            return View(ekwipunek);
        }

        //Delete

        public async Task<IActionResult> Delete(int id, int postacId)
        {
            var ekwipunek = await _context.Ekwipunki.FirstOrDefaultAsync(e => e.Id == id && e.PostacId == postacId);

            if (ekwipunek == null)
            {
                return NotFound("Nie znaleziono ekwipunku.");
            }

            return View(ekwipunek);
        }

        //Delete Post Confirmed

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int postacId)
        {
            var ekwipunek = await _context.Ekwipunki.FirstOrDefaultAsync(e => e.Id == id && e.PostacId == postacId);

            if (ekwipunek != null)
            {
                _context.Ekwipunki.Remove(ekwipunek);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Postac", new { id = postacId });
        }
    }
}
