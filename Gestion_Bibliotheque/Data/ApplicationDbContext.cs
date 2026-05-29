using Microsoft.EntityFrameworkCore;
using Gestion_Bibliotheque.Models;

namespace Gestion_Bibliotheque.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<Penalite> Penalites { get; set; }
        public DbSet<Abonnement> Abonnements { get; set; }
        /*public DbSet<Utilisateur> Utilisateurs { get; set; }
       public DbSet<Alerte> Alertes { get; set; }
       */
    }
}