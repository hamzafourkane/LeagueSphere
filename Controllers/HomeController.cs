using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeagueSphere.Models;
using LeagueSphere.Data;
using Microsoft.EntityFrameworkCore;

namespace LeagueSphere.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // ====================================================
            // SCÉNARIO 1 : ADMINISTRATEUR -> DASHBOARD DE GESTION
            // ====================================================
            if (User.IsInRole("Admin"))
            {
                int totalMatchs = await _context.Matchs.CountAsync();
                int matchsJoues = await _context.Matchs.Where(m => m.EstTermine).CountAsync();

                ViewBag.NbEquipes = await _context.Equipes.CountAsync();
                ViewBag.NbMatchs = totalMatchs;
                ViewBag.NbMatchsJoues = matchsJoues;
                ViewBag.Progression = totalMatchs > 0 ? (int)((double)matchsJoues / totalMatchs * 100) : 0;
                ViewBag.TotalButs = await _context.Matchs.Where(m => m.EstTermine).SumAsync(m => m.ScoreDomicile + m.ScoreExterieur);

                // Identifier le leader du championnat
                var leader = await _context.Equipes.OrderByDescending(e => e.Points)
                                                   .ThenByDescending(e => (e.ButsPour - e.ButsContre))
                                                   .FirstOrDefaultAsync();

                // 5 derniers matchs terminés
                ViewBag.DerniersMatchs = await _context.Matchs
                                               .Include(m => m.EquipeDomicile).Include(m => m.EquipeExterieur)
                                               .Where(m => m.EstTermine)
                                               .OrderByDescending(m => m.DateMatch).Take(5).ToListAsync();

                return View("Dashboard", leader);
            }

            // ====================================================
            // SCÉNARIO 2 : SUPPORTER / PUBLIC -> FLUX SOCIAL & PRONOSTICS
            // ====================================================
            var posts = await _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Commentaires)
                .OrderByDescending(p => p.DatePublication)
                .ToListAsync();

            var matchsFinis = await _context.Matchs
                .Include(m => m.EquipeDomicile).Include(m => m.EquipeExterieur)
                .Where(m => m.EstTermine)
                .OrderByDescending(m => m.DateMatch)
                .Take(3)
                .ToListAsync();

            var matchsFuturs = await _context.Matchs
                .Include(m => m.EquipeDomicile).Include(m => m.EquipeExterieur)
                .Where(m => !m.EstTermine)
                .OrderBy(m => m.DateMatch)
                .Take(3)
                .ToListAsync();

            var model = new FeedViewModel
            {
                Posts = posts,
                MatchsRecents = matchsFinis,
                MatchsAVenir = matchsFuturs
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}