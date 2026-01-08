using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;

namespace FitnessWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsApiController : ControllerBase
    {
        private readonly FitnessContext _context;
        public BookingsApiController(FitnessContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            var session = await _context.Session.Include(s => s.Bookings).FirstOrDefaultAsync(s => s.ID == request.SessionID);
            if (session == null) return NotFound("Session not found");

            if ((session.Bookings?.Count ?? 0) >= session.MaxCapacity)
                return BadRequest("Class is full.");

            var member = await _context.Member.FirstOrDefaultAsync(m => m.Email == request.UserEmail);
            if (member == null) return BadRequest("Member profile not found.");

            bool alreadyBooked = session.Bookings.Any(b => b.MemberID == member.ID);
            if (alreadyBooked) return BadRequest("You are already booked for this class.");

            var booking = new Booking { SessionID = request.SessionID, MemberID = member.ID };
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return Ok("Booking confirmed");
        }
    }

    public class BookingRequest
    {
        public int SessionID { get; set; }
        public string UserEmail { get; set; }
    }
}