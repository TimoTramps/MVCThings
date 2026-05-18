using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingLog.Data;

namespace TrainingLog.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly TrainingLogContext _context;

        public ApiController(TrainingLogContext context)
        {
            _context = context;
        }

        // GET: api/athletes
        [HttpGet("athletes")]
        public async Task<IActionResult> GetAthletes()
        {
            var athletes = await _context.Athlete.ToListAsync();
            return Ok(athletes);
        }

        // GET: api/workouts
        [HttpGet("workouts")]
        public async Task<IActionResult> GetWorkouts()
        {
            var workouts = await _context.Workouts.Include(w => w.Athlete).ToListAsync();
            return Ok(workouts);
        }

        // GET: api/exercises
        [HttpGet("exercises")]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _context.Exercises.Include(e => e.Workout).ToListAsync();
            return Ok(exercises);
        }
    }
}