namespace FitnessWeb.Models
{
    public class Trainer
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }

        public ICollection<TrainerSpecialization>? TrainerSpecializations { get; set; }
        public ICollection<Session>? Sessions { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
