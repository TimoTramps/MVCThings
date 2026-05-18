using System;
using System.Collections.Generic;

namespace TrainingLog.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public string? Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float WeightKg { get; set; }

        public Workout? Workout { get; set; }
    }
}
