using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueSphere.Data;
using LeagueSphere.Models;
using Microsoft.AspNetCore.Authorization;

namespace LeagueSphere.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.OrderByDescending(p => p.DatePublication).ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts
                .Include(p => p.Commentaires)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null) return NotFound();

            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Titre,Contenu,ImageUrl")] Post post)
        {
            post.DatePublication = DateTime.Now;
            post.Auteur = User.Identity?.Name ?? "Supporter";

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Contenu,ImageUrl,DatePublication,Auteur")] Post post)
        {
            if (id != post.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null) return NotFound();

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        // POST: Posts/AjouterCommentaire
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterCommentaire(int PostId, string Contenu, string? Auteur)
        {
            if (PostId == 0 || string.IsNullOrWhiteSpace(Contenu))
            {
                return RedirectToAction(nameof(Details), new { id = PostId });
            }

            var authorName = User.Identity?.IsAuthenticated == true
                ? User.Identity.Name
                : (!string.IsNullOrWhiteSpace(Auteur) ? Auteur : "Supporter");

            var commentaire = new Commentaire
            {
                PostId = PostId,
                Contenu = Contenu.Trim(),
                Auteur = authorName ?? "Supporter",
                DateCommentaire = DateTime.Now
            };

            _context.Commentaires.Add(commentaire);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = PostId });
        }

        // POST: Posts/ToggleLike
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            var existingLike = await _context.PostLikes
                .FirstOrDefaultAsync(l => l.PostId == id && l.UserId == username);

            if (existingLike != null)
            {
                _context.PostLikes.Remove(existingLike);
            }
            else
            {
                _context.PostLikes.Add(new PostLike
                {
                    PostId = id,
                    UserId = username
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
