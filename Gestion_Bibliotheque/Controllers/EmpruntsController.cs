using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Bibliotheque.Data;
using Gestion_Bibliotheque.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gestion_Bibliotheque.Controllers
{
    public class EmpruntsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpruntsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // LISTE DES EMPRUNTS
        // =========================

        public async Task<IActionResult> Index()
        {
            var emprunts = _context.Emprunts
                .Include(e => e.Livre)
                .Include(e => e.Membre);

            return View(await emprunts.ToListAsync());
        }

        // =========================
        // DETAILS
        // =========================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts
                .FirstOrDefaultAsync(e => e.id_emprunt == id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return View(emprunt);
        }

        // =========================
        // CREATE GET
        // =========================

        public IActionResult Create()
        {
            ViewBag.Livres = new SelectList(
                _context.Livres,
                "id_livres",
                "titre"
            );

            ViewBag.Membres = new SelectList(
                _context.Membres,
                "id_membres",
                "nom_membres"
            );

            return View();
        }

        // =========================
        // CREATE POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("id_emprunt,id_livres,id_membres,date_retourprevue")]
    Emprunt emprunt)
        {
            // Vérifier abonnement valide

            var abonnement = _context.Abonnements
                .FirstOrDefault(a =>
                    a.id_membres == emprunt.id_membres
                    &&
                    a.date_finabonnement >= DateTime.Now);

            if (abonnement == null)
            {
                ViewBag.Message =
                    "Le membre n'a pas d'abonnement valide.";

                ViewBag.Livres =
                    new SelectList(
                        _context.Livres,
                        "id_livres",
                        "titre");

                ViewBag.Membres =
                    new SelectList(
                        _context.Membres,
                        "id_membres",
                        "nom_membres");

                return View(emprunt);
            }

            if (ModelState.IsValid)
            {
                _context.Emprunts.Add(emprunt);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Livres =
                new SelectList(
                    _context.Livres,
                    "id_livres",
                    "titre");

            ViewBag.Membres =
                new SelectList(
                    _context.Membres,
                    "id_membres",
                    "nom_membres");

            return View(emprunt);
        
        }

        // =========================
        // EDIT POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Emprunt emprunt)
        {
            if (id != emprunt.id_emprunt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprunt);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Emprunts.Any(e => e.id_emprunt == emprunt.id_emprunt))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(emprunt);
        }

        // =========================
        // DELETE GET
        // =========================

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts
                .FirstOrDefaultAsync(e => e.id_emprunt == id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return View(emprunt);
        }

        // =========================
        // DELETE POST
        // =========================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);

            if (emprunt != null)
            {
                _context.Emprunts.Remove(emprunt);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // RETOURNER LIVRE
        // =========================

        public async Task<IActionResult> Retourner(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);

            if (emprunt == null)
            {
                return NotFound();
            }

            // 🔥 ÉVITER DOUBLE RETOUR
            if (emprunt.statut == "Retourné")
            {
                return RedirectToAction(nameof(Index));
            }

            // 🔥 DATE RETOUR
            emprunt.date_retoureffective = DateTime.Now;

            // 🔥 STATUT
            emprunt.statut = "Retourné";

            _context.Update(emprunt);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GenererPenalite(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts
                .FirstOrDefaultAsync(e =>
                    e.id_emprunt == id);

            if (emprunt == null)
            {
                return NotFound();
            }

            // Vérifier retard

            if (DateTime.Now <= emprunt.date_retourprevue)
            {
                TempData["Message"] =
                "Aucun retard détecté.";

                return RedirectToAction(nameof(Index));
            }

            // Calcul retard

            int joursRetard =
            (DateTime.Now - emprunt.date_retourprevue).Days;

            decimal montant =
            joursRetard * 500;

            // Créer pénalité

            Penalite penalite = new Penalite()
            {
                date_penalite = DateTime.Now,

                raison_penalite =
                $"Retard de {joursRetard} jour(s)",

                montant_penalite = montant,

                id_emprunt = emprunt.id_emprunt
            };

            _context.Penalites.Add(penalite);

            await _context.SaveChangesAsync();

            TempData["Message"] =
            "Pénalité générée avec succès.";

            return RedirectToAction(nameof(Index));
        }
    }
   
    }