using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bibliotheque.Models
{
    public class Abonnement
    {
        [Key]
        public int id_abonnement { get; set; }

        public DateTime date_debutabonnement { get; set; }

        public string? type_abonnement { get; set; }

        public DateTime date_finabonnement { get; set; }

        public int id_membres { get; set; }

        [ForeignKey("id_membres")]
        public Membre? Membre { get; set; }
    }
}