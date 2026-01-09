using System.ComponentModel.DataAnnotations;

namespace FitnessWeb.Models
{
    public class Session
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [FutureDate] 
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        [Range(1, 5, ErrorMessage = "Capacity must be between 1 and 5")]
        public int MaxCapacity { get; set; }

        public int TrainerID { get; set; }
        public Trainer? Trainer { get; set; }

        public int WorkoutTypeID { get; set; }
        public WorkoutType? WorkoutType { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}