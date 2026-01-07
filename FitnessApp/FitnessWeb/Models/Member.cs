namespace FitnessWeb.Models
{
    public class Member
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
