namespace FitnessWeb.DTOs
{
    public class TrainerDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; } //lista unita
        public string Description { get; set; }
        public string Email { get; set; }
        public double AverageRating { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
    }
}