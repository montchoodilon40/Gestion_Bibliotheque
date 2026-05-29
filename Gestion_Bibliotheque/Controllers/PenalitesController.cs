
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Bibliotheque.Models;
using Gestion_Bibliotheque.Data;

public class PenalitesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PenalitesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PENALITES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Penalites.ToListAsync());
    }

    // GET: PENALITES/Details/5
    public async Task<IActionResult> Details(int? id_penalite)
    {
        if (id_penalite == null)
        {
            return NotFound();
        }

        var penalite = await _context.Penalites
            .FirstOrDefaultAsync(m => m.id_penalite == id_penalite);
        if (penalite == null)
        {
            return NotFound();
        }

        return View(penalite);
    }

    // GET: PENALITES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PENALITES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id_penalite,date_penalite,raison_penalite,montant_penalite,id_emprunt,Emprunt")] Penalite penalite)
    {
        if (ModelState.IsValid)
        {
            _context.Add(penalite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(penalite);
    }

    // GET: PENALITES/Edit/5
    public async Task<IActionResult> Edit(int? id_penalite)
    {
        if (id_penalite == null)
        {
            return NotFound();
        }

        var penalite = await _context.Penalites.FindAsync(id_penalite);
        if (penalite == null)
        {
            return NotFound();
        }
        return View(penalite);
    }

    // POST: PENALITES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id_penalite, [Bind("id_penalite,date_penalite,raison_penalite,montant_penalite,id_emprunt,Emprunt")] Penalite penalite)
    {
        if (id_penalite != penalite.id_penalite)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(penalite);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaliteExists(penalite.id_penalite))
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
        return View(penalite);
    }

    // GET: PENALITES/Delete/5
    public async Task<IActionResult> Delete(int? id_penalite)
    {
        if (id_penalite == null)
        {
            return NotFound();
        }

        var penalite = await _context.Penalites
            .FirstOrDefaultAsync(m => m.id_penalite == id_penalite);
        if (penalite == null)
        {
            return NotFound();
        }

        return View(penalite);
    }

    // POST: PENALITES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id_penalite)
    {
        var penalite = await _context.Penalites.FindAsync(id_penalite);
        if (penalite != null)
        {
            _context.Penalites.Remove(penalite);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PenaliteExists(int? id_penalite)
    {
        return _context.Penalites.Any(e => e.id_penalite == id_penalite);
    }
}
