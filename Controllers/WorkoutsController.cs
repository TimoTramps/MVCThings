using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingLog.Data;
using TrainingLog.Models;

namespace TrainingLog.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly TrainingLogContext _context;

        public WorkoutsController(TrainingLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Workouts.Include(w => w.Athlete).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var workout = await _context.Workouts.Include(w => w.Athlete).FirstOrDefaultAsync(m => m.WorkoutId == id);
            if (workout == null) return NotFound();
            return View(workout);
        }

        public IActionResult Create()
        {
            ViewBag.Athletes = _context.Athlete.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutId,AthleteId,Date,DurationMinutes,Notes")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Athletes = _context.Athlete.ToList();
            return View(workout);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null) return NotFound();
            ViewBag.Athletes = _context.Athlete.ToList();
            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutId,AthleteId,Date,DurationMinutes,Notes")] Workout workout)
        {
            if (id != workout.WorkoutId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.WorkoutId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Athletes = _context.Athlete.ToList();
            return View(workout);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var workout = await _context.Workouts.Include(w => w.Athlete).FirstOrDefaultAsync(m => m.WorkoutId == id);
            if (workout == null) return NotFound();
            return View(workout);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout != null) _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}