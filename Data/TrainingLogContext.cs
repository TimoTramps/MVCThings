using Microsoft.EntityFrameworkCore;
using TrainingLog.Models;

namespace TrainingLog.Data
{
    public class TrainingLogContext : DbContext
    {
        public TrainingLogContext(DbContextOptions<TrainingLogContext> options)
            : base(options)
        {
        }

        public DbSet<Athlete> Athlete { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
    }
}