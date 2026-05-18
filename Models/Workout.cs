using System;
using System.Collections.Generic;

namespace TrainingLog.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public int AthleteId { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
        public string? Notes { get; set; }

        public Athlete? Athlete { get; set; }
        public ICollection<Exercise>? Exercises { get; set; }
    }
}
