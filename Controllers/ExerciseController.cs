using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingLog.Data;
using TrainingLog.Models;

namespace TrainingLog.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly TrainingLogContext _context;

        public ExercisesController(TrainingLogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Exercises.Include(e => e.Workout).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var exercise = await _context.Exercises.Include(e => e.Workout).FirstOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null) return NotFound();
            return View(exercise);
        }

        public IActionResult Create()
        {
            ViewBag.Workouts = _context.Workouts.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,WorkoutId,Name,Sets,Reps,WeightKg")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Workouts = _context.Workouts.ToList();
            return View(exercise);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return NotFound();
            ViewBag.Workouts = _context.Workouts.ToList();
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,WorkoutId,Name,Sets,Reps,WeightKg")] Exercise exercise)
        {
            if (id != exercise.ExerciseId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Workouts = _context.Workouts.ToList();
            return View(exercise);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var exercise = await _context.Exercises.Include(e => e.Workout).FirstOrDefaultAsync(m => m.ExerciseId == id);
            return View(exercise);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null) _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}
