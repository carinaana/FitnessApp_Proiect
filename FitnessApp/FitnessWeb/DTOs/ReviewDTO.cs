namespace FitnessWeb.DTOs
{
    public class ReviewDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string MemberName { get; set; } 
        public DateTime Date { get; set; }
    }
}