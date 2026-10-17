using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueSphere.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int VoteDomicile { get; set; } = 0;
        public int VoteNul { get; set; } = 0;
        public int VoteExterieur { get; set; } = 0;

        [Required]
        [Display(Name = "Date du match")]
        public DateTime DateMatch { get; set; }

        // Relations Clés Étrangères
        [Display(Name = "Équipe Domicile")]
        public int EquipeDomicileId { get; set; }
        [ForeignKey("EquipeDomicileId")]
        public virtual Equipe? EquipeDomicile { get; set; }

        [Display(Name = "Équipe Extérieur")]
        public int EquipeExterieurId { get; set; }
        [ForeignKey("EquipeExterieurId")]
        public virtual Equipe? EquipeExterieur { get; set; }

        [Display(Name = "Score Domicile")]
        public int ScoreDomicile { get; set; }

        [Display(Name = "Score Extérieur")]
        public int ScoreExterieur { get; set; }

        public bool EstTermine { get; set; } = false;
    }
}