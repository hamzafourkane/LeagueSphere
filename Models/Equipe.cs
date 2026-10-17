using System.ComponentModel.DataAnnotations;

namespace LeagueSphere.Models
{
    public class Equipe
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom de l'équipe")]
        public string Nom { get; set; } = string.Empty;

        [Display(Name = "Logo (URL)")]
        public string? LogoUrl { get; set; }

        // Statistiques
        public int Points { get; set; } = 0;
        public int MatchsJoues { get; set; } = 0;
        public int Victoires { get; set; } = 0;
        public int Defaites { get; set; } = 0;
        public int Nuls { get; set; } = 0;
        public int ButsPour { get; set; } = 0;
        public int ButsContre { get; set; } = 0;

        public int Diff => ButsPour - ButsContre;
    }
}