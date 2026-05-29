
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Bibliotheque.Models;
using Gestion_Bibliotheque.Data;

public class LivresController : Controller
{
    private readonly ApplicationDbContext _context;

    public LivresController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: LIVRES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Livres.ToListAsync());
    }

    // GET: LIVRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livre = await _context.Livres
            .FirstOrDefaultAsync(m => m.id_livres == id);
        if (livre == null)
        {
            return NotFound();
        }

        return View(livre);
    }

    // GET: LIVRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LIVRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id_livres,titre,auteur,categorie,quantite,disponibilite")] Livre livre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(livre);
    }

    // GET: LIVRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id== null)
        {
            return NotFound();
        }

        var livre = await _context.Livres.FindAsync(id);
        if (livre == null)
        {
            return NotFound();
        }
        return View(livre);
    }

    // POST: LIVRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id_livres, [Bind("id_livres,titre,auteur,categorie,quantite,disponibilite")] Livre livre)
    {
        if (id_livres != livre.id_livres)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(livre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(livre.id_livres))
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
        return View(livre);
    }

    // GET: LIVRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livre = await _context.Livres
            .FirstOrDefaultAsync(m => m.id_livres == id);
        if (livre == null)
        {
            return NotFound();
        }

        return View(livre);
    }

    // POST: LIVRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id_livres)
    {
        var livre = await _context.Livres.FindAsync(id_livres);
        if (livre != null)
        {
            _context.Livres.Remove(livre);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LivreExists(int? id_livres)
    {
        return _context.Livres.Any(e => e.id_livres == id_livres);
    }
}
