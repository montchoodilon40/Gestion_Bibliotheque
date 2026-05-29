
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Bibliotheque.Models;
using Gestion_Bibliotheque.Data;

public class AbonnementsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AbonnementsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ABONNEMENTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Abonnements.ToListAsync());
    }

    // GET: ABONNEMENTS/Details/5
    public async Task<IActionResult> Details(int? id_abonnement)
    {
        if (id_abonnement == null)
        {
            return NotFound();
        }

        var abonnement = await _context.Abonnements
            .FirstOrDefaultAsync(m => m.id_abonnement == id_abonnement);
        if (abonnement == null)
        {
            return NotFound();
        }

        return View(abonnement);
    }

    // GET: ABONNEMENTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ABONNEMENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id_abonnement,date_debutabonnement,type_abonnement,date_finabonnement,id_membres,Membre")] Abonnement abonnement)
    {
        if (ModelState.IsValid)
        {
            _context.Add(abonnement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(abonnement);
    }

    // GET: ABONNEMENTS/Edit/5
    public async Task<IActionResult> Edit(int? id_abonnement)
    {
        if (id_abonnement == null)
        {
            return NotFound();
        }

        var abonnement = await _context.Abonnements.FindAsync(id_abonnement);
        if (abonnement == null)
        {
            return NotFound();
        }
        return View(abonnement);
    }

    // POST: ABONNEMENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id_abonnement, [Bind("id_abonnement,date_debutabonnement,type_abonnement,date_finabonnement,id_membres,Membre")] Abonnement abonnement)
    {
        if (id_abonnement != abonnement.id_abonnement)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(abonnement);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonnementExists(abonnement.id_abonnement))
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
        return View(abonnement);
    }

    // GET: ABONNEMENTS/Delete/5
    public async Task<IActionResult> Delete(int? id_abonnement)
    {
        if (id_abonnement == null)
        {
            return NotFound();
        }

        var abonnement = await _context.Abonnements
            .FirstOrDefaultAsync(m => m.id_abonnement == id_abonnement);
        if (abonnement == null)
        {
            return NotFound();
        }

        return View(abonnement);
    }

    // POST: ABONNEMENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id_abonnement)
    {
        var abonnement = await _context.Abonnements.FindAsync(id_abonnement);
        if (abonnement != null)
        {
            _context.Abonnements.Remove(abonnement);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AbonnementExists(int? id_abonnement)
    {
        return _context.Abonnements.Any(e => e.id_abonnement == id_abonnement);
    }
}
