using System.ComponentModel.DataAnnotations;

namespace FitnessWeb.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // nota de la 1 la 5

        [StringLength(500)]
        public string? Comment { get; set; } 

        public string MemberName { get; set; } 

        public DateTime Date { get; set; } = DateTime.Now;

        public int TrainerID { get; set; }
        public Trainer? Trainer { get; set; }
    }
}