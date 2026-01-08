using System.ComponentModel.DataAnnotations;

namespace FitnessWeb.Models
{
    public class Session
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Start Time")]
        public string StartTime { get; set; } 
        public int DurationMinutes { get; set; } 
        public int MaxCapacity { get; set; }

        public int TrainerID { get; set; }
        public Trainer? Trainer { get; set; }

        public int WorkoutTypeID { get; set; }
        public WorkoutType? WorkoutType { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
