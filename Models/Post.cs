using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeagueSphere.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titre")]
        public string Titre { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Message")]
        public string Contenu { get; set; } = string.Empty;

        [Display(Name = "Image (URL facultative)")]
        public string? ImageUrl { get; set; }

        public DateTime DatePublication { get; set; } = DateTime.Now;
        public string Auteur { get; set; } = "Anonyme";

        // Relation Commentaires
        public virtual List<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

        // Relation Likes
        public virtual ICollection<PostLike> Likes { get; set; } = new List<PostLike>();
    }
}