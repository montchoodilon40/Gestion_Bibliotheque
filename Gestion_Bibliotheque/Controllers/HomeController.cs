using Gestion_Bibliotheque.Data;
using Gestion_Bibliotheque.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gestion_Bibliotheque.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalLivres = _context.Livres.Count();

            ViewBag.Disponibles = _context.Livres
                .Count(l => l.disponibilite == true);

            ViewBag.Indisponibles = _context.Livres
                .Count(l => l.disponibilite == false);

            ViewBag.TotalQuantite = _context.Livres
                .Sum(l => l.quantite);

            return View();
        }
    }
}

