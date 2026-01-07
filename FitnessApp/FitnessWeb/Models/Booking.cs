namespace FitnessWeb.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public int MemberID { get; set; }
        public Member? Member { get; set; }

        public int SessionID { get; set; }
        public Session? Session { get; set; }
    }
}
