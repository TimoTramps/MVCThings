using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingLog.Data;
using TrainingLog.Models;

namespace TrainingLog.Controllers
{
    public class AthletesController : Controller
    {
        private readonly TrainingLogContext _context;

        public AthletesController(TrainingLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Athlete.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var athlete = await _context.Athlete.FirstOrDefaultAsync(m => m.AthleteId == id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AthleteId,Name,Age,Position,CreatedAt")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(athlete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(athlete);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var athlete = await _context.Athlete.FindAsync(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AthleteId,Name,Age,Position,CreatedAt")] Athlete athlete)
        {
            if (id != athlete.AthleteId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(athlete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteExists(athlete.AthleteId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(athlete);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var athlete = await _context.Athlete.FirstOrDefaultAsync(m => m.AthleteId == id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var athlete = await _context.Athlete.FindAsync(id);
            if (athlete != null) _context.Athlete.Remove(athlete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AthleteExists(int id)
        {
            return _context.Athlete.Any(e => e.AthleteId == id);
        }
    }
}