using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bibliotheque.Models
{
    [Table("livres")]
    public class Livre
    {
        [Key]
        public int id_livres { get; set; }

        public string? titre { get; set; }

        public string? auteur { get; set; }

        public string? categorie { get; set; }

        public int quantite { get; set; }

        public bool disponibilite { get; set; }
    }
}