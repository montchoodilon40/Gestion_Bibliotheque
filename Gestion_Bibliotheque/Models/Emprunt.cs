using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Bibliotheque.Models
{
    public class Emprunt
    {
        [Key]
        public int id_emprunt { get; set; }

        public int id_livres { get; set; }

        public int id_membres { get; set; }

        public DateTime date_emprunt { get; set; }

        public DateTime date_retourprevue { get; set; }
        public DateTime? date_retoureffective { get; set; }

        public string? statut { get; set; }
        [ForeignKey("id_livres")]
        public Livre Livre { get; set; }

        [ForeignKey("id_membres")]
        public Membre Membre { get; set; }
    }


}