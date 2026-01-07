namespace FitnessWeb.Models
{
    public class WorkoutType
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public string Difficulty { get; set; } 
        public ICollection<Session>? Sessions { get; set; }
    }
}
