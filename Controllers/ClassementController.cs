using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueSphere.Data;

namespace LeagueSphere.Controllers
{
    public class ClassementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Tri par Points décroissants, puis par Différence de buts, puis meilleure attaque
            var classement = await _context.Equipes
                .OrderByDescending(e => e.Points)
                .ThenByDescending(e => (e.ButsPour - e.ButsContre))
                .ThenByDescending(e => e.ButsPour)
                .ToListAsync();

            return View(classement);
        }
    }
}