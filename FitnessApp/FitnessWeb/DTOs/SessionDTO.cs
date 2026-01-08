namespace FitnessWeb.DTOs
{
    public class SessionDTO
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } 
        public string StartTime { get; set; }
        public string WorkoutName { get; set; }
        public string TrainerName { get; set; }
        public int AvailableSpots { get; set; } 
    }
}