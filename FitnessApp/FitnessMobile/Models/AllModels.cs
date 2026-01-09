using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessMobile.Models
{
    // Pentru Login/Register
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Pentru Antrenori
    public class TrainerDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double AverageRating { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }

    public class ReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string MemberName { get; set; }
        public DateTime Date { get; set; }
    }

    // Pentru Sporturi si Sesiuni
    public class WorkoutType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
    }

    public class SessionDto
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } // Data completa
        public string StartTime { get; set; } // Doar ora string
        public string WorkoutName { get; set; }
        public string TrainerName { get; set; }
        public int AvailableSpots { get; set; }
    }

    // Pentru Rezervare
    public class BookingRequest
    {
        public int SessionID { get; set; }
        public string UserEmail { get; set; }
    }
}

