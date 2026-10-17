using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueSphere.Data;
using LeagueSphere.Models;
using Microsoft.AspNetCore.Authorization;

namespace LeagueSphere.Controllers
{
    public class EquipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipes.ToListAsync());
        }

        // GET: Equipes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Nom,LogoUrl")] Equipe equipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipe);
        }

        // GET: Equipes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var equipe = await _context.Equipes.FindAsync(id);
            if (equipe == null) return NotFound();

            return View(equipe);
        }

        // POST: Equipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,LogoUrl,Points,MatchsJoues,Victoires,Defaites,Nuls,ButsPour,ButsContre")] Equipe equipe)
        {
            if (id != equipe.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Equipes.Any(e => e.Id == equipe.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipe);
        }

        // GET: Equipes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var equipe = await _context.Equipes.FirstOrDefaultAsync(m => m.Id == id);
            if (equipe == null) return NotFound();

            return View(equipe);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipe = await _context.Equipes.FindAsync(id);
            if (equipe != null)
            {
                _context.Equipes.Remove(equipe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}