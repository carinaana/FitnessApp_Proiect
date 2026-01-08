namespace FitnessWeb.Models
{
    public class TrainerSpecialization
    {
        public int ID { get; set; }

        public int TrainerID { get; set; }
        public Trainer Trainer { get; set; }

        public int WorkoutTypeID { get; set; }
        public WorkoutType WorkoutType { get; set; }
    }
}