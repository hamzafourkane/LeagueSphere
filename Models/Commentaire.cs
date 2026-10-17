using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueSphere.Models
{
    public class Commentaire
    {
        public int Id { get; set; }

        [Required]
        public string Contenu { get; set; } = string.Empty;

        public string Auteur { get; set; } = "Supporter";

        public DateTime DateCommentaire { get; set; } = DateTime.Now;

        // Lien vers le Post
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post? Post { get; set; }
    }
}