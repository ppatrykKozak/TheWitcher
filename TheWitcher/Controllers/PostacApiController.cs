using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWitcher.Data;
using TheWitcher.Data.Data;
using TheWitcher.Data.Models;

namespace TheWitcher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostacApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostacApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PostacApi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Użyj Include, aby załadować ekwipunek wraz z postaciami
            var postacie = await _context.Postacie
                                           .Include(p => p.Ekwipunek)  // Zawiera ekwipunek
                                           .ToListAsync();
            return Ok(postacie);
        }

        // GET: api/PostacApi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var postac = await _context.Postacie
                                        .Include(p => p.Ekwipunek) // Ładowanie powiązanego ekwipunku
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (postac == null)
                return NotFound();

            return Ok(postac);
        }
        // POST: api/PostacApi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Postac postac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Postacie.Add(postac);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = postac.Id }, postac);
        }

        // PUT: api/PostacApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Postac updatedPostac)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPostac = await _context.Postacie.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPostac == null)
                return NotFound();

            existingPostac.Imie = updatedPostac.Imie;
            existingPostac.Poziom = updatedPostac.Poziom;
            existingPostac.Umiejetnosci = updatedPostac.Umiejetnosci;
            existingPostac.RasaId = updatedPostac.RasaId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PostacApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var postac = await _context.Postacie.FirstOrDefaultAsync(p => p.Id == id);
            if (postac == null)
                return NotFound();

            _context.Postacie.Remove(postac);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
