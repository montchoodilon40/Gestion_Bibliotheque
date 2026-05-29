
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Bibliotheque.Models;
using Gestion_Bibliotheque.Data;

public class MembresController : Controller
{
    private readonly ApplicationDbContext _context;

    public MembresController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: MEMBRES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Membres.ToListAsync());
    }

    // GET: MEMBRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membre = await _context.Membres
            .FirstOrDefaultAsync(m => m.id_membres == id);
        if (membre == null)
        {
            return NotFound();
        }

        return View(membre);
    }

    // GET: MEMBRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MEMBRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id_membres,nom_membres,prenom_membres,email_membres,tel_membres,adresse_membres")] Membre membre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(membre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(membre);
    }

    // GET: MEMBRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id== null)
        {
            return NotFound();
        }

        var membre = await _context.Membres.FindAsync(id);
        if (membre == null)
        {
            return NotFound();
        }
        return View(membre);
    }

    // POST: MEMBRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id_membres, [Bind("id_membres,nom_membres,prenom_membres,email_membres,tel_membres,adresse_membres")] Membre membre)
    {
        if (id_membres != membre.id_membres)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(membre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembreExists(membre.id_membres))
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
        return View(membre);
    }

    // GET: MEMBRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membre = await _context.Membres
            .FirstOrDefaultAsync(m => m.id_membres == id);
        if (membre == null)
        {
            return NotFound();
        }

        return View(membre);
    }

    // POST: MEMBRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id_membres)
    {
        var membre = await _context.Membres.FindAsync(id_membres);
        if (membre != null)
        {
            _context.Membres.Remove(membre);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MembreExists(int? id_membres)
    {
        return _context.Membres.Any(e => e.id_membres == id_membres);
    }
}
