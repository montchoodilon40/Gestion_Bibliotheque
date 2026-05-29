using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bibliotheque.Models
{
    [Table("membres")]
    public class Membre
    {
        [Key]
        public int id_membres { get; set; }

        public string? nom_membres { get; set; }

        public string? prenom_membres { get; set; }

        public string? email_membres { get; set; }

        public string? tel_membres { get; set; }

        public string? adresse_membres { get; set; }
    }
}