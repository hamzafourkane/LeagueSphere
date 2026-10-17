using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeagueSphere.Data;
using LeagueSphere.Models;
using Microsoft.AspNetCore.Authorization;

namespace LeagueSphere.Controllers
{
    public class MatchsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matchs
        public async Task<IActionResult> Index()
        {
            var matchs = await _context.Matchs
                .Include(m => m.EquipeDomicile)
                .Include(m => m.EquipeExterieur)
                .OrderBy(m => m.DateMatch)
                .ToListAsync();

            return View(matchs);
        }

        // GET: Matchs/Create (Admin seulement)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EquipeDomicileId"] = new SelectList(_context.Equipes, "Id", "Nom");
            ViewData["EquipeExterieurId"] = new SelectList(_context.Equipes, "Id", "Nom");
            return View();
        }

        // POST: Matchs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,DateMatch,EquipeDomicileId,EquipeExterieurId")] Match match)
        {
            if (match.EquipeDomicileId == match.EquipeExterieurId)
            {
                ModelState.AddModelError("EquipeExterieurId", "L'équipe extérieur doit être différente de l'équipe domicile.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipeDomicileId"] = new SelectList(_context.Equipes, "Id", "Nom", match.EquipeDomicileId);
            ViewData["EquipeExterieurId"] = new SelectList(_context.Equipes, "Id", "Nom", match.EquipeExterieurId);
            return View(match);
        }

        // GET: Matchs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matchs.FindAsync(id);
            if (match == null) return NotFound();

            ViewData["EquipeDomicileId"] = new SelectList(_context.Equipes, "Id", "Nom", match.EquipeDomicileId);
            ViewData["EquipeExterieurId"] = new SelectList(_context.Equipes, "Id", "Nom", match.EquipeExterieurId);
            return View(match);
        }

        // POST: Matchs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateMatch,EquipeDomicileId,EquipeExterieurId,ScoreDomicile,ScoreExterieur,EstTermine")] Match match)
        {
            if (id != match.Id) return NotFound();

            if (match.EquipeDomicileId == match.EquipeExterieurId)
            {
                ModelState.AddModelError("EquipeExterieurId", "L'équipe extérieur doit être différente de l'équipe domicile.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();

                    // Recalcule le classement général après modification
                    await RecalculerClassement();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipeDomicileId"] = new SelectList(_context.Equipes, "Id", "Nom", match.EquipeDomicileId);
            ViewData["EquipeExterieurId"] = new SelectList(_context.Equipes, "Id", "Nom", match.EquipeExterieurId);
            return View(match);
        }

        // Recalcule les points et statistiques de toutes les équipes
        private async Task RecalculerClassement()
        {
            var equipes = await _context.Equipes.ToListAsync();
            var matchs = await _context.Matchs.Where(m => m.EstTermine).ToListAsync();

            // 1. Remise à zéro
            foreach (var eq in equipes)
            {
                eq.Points = 0;
                eq.MatchsJoues = 0;
                eq.Victoires = 0;
                eq.Nuls = 0;
                eq.Defaites = 0;
                eq.ButsPour = 0;
                eq.ButsContre = 0;
            }

            // 2. Calcul des statistiques
            foreach (var m in matchs)
            {
                var dom = equipes.FirstOrDefault(e => e.Id == m.EquipeDomicileId);
                var ext = equipes.FirstOrDefault(e => e.Id == m.EquipeExterieurId);

                if (dom != null && ext != null)
                {
                    dom.MatchsJoues++;
                    ext.MatchsJoues++;
                    dom.ButsPour += m.ScoreDomicile;
                    dom.ButsContre += m.ScoreExterieur;
                    ext.ButsPour += m.ScoreExterieur;
                    ext.ButsContre += m.ScoreDomicile;

                    if (m.ScoreDomicile > m.ScoreExterieur)
                    {
                        dom.Points += 3;
                        dom.Victoires++;
                        ext.Defaites++;
                    }
                    else if (m.ScoreDomicile < m.ScoreExterieur)
                    {
                        ext.Points += 3;
                        ext.Victoires++;
                        dom.Defaites++;
                    }
                    else
                    {
                        dom.Points += 1;
                        ext.Points += 1;
                        dom.Nuls++;
                        ext.Nuls++;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        // GET: Matchs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var match = await _context.Matchs
                .Include(m => m.EquipeDomicile)
                .Include(m => m.EquipeExterieur)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();

            return View(match);
        }

        // POST: Matchs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matchs.FindAsync(id);
            if (match != null)
            {
                _context.Matchs.Remove(match);
                await _context.SaveChangesAsync();
                await RecalculerClassement();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matchs.Any(e => e.Id == id);
        }

        // POST: Matchs/Voter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Voter(int id, string choix)
        {
            var match = await _context.Matchs.FindAsync(id);
            if (match != null)
            {
                switch (choix?.ToLower())
                {
                    case "domicile":
                        match.VoteDomicile++;
                        break;
                    case "nul":
                        match.VoteNul++;
                        break;
                    case "exterieur":
                        match.VoteExterieur++;
                        break;
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}