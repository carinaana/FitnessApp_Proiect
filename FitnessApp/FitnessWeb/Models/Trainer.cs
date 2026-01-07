namespace FitnessWeb.Models
{
    public class Trainer
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; } 
        public ICollection<Session>? Sessions { get; set; }
    }
}
