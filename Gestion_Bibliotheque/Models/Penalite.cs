using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bibliotheque.Models
{
    public class Penalite
    {
        [Key]
        public int id_penalite { get; set; }

        public DateTime date_penalite { get; set; }

        public string? raison_penalite { get; set; }

        public decimal montant_penalite { get; set; }

        public int id_emprunt { get; set; }

        [ForeignKey("id_emprunt")]
        public Emprunt? Emprunt { get; set; }
    }
}