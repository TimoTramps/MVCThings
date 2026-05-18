using System;
using System.Collections.Generic;

namespace TrainingLog.Models
{
    public class Athlete
    {
        public int AthleteId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Position { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Workout>? Workouts { get; set; }
    }
}
