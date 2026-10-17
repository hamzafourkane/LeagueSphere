using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeagueSphere.Models;

namespace LeagueSphere.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Empêche les suppressions en cascade orphelines
            builder.Entity<Match>()
                .HasOne(m => m.EquipeDomicile)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.EquipeExterieur)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}