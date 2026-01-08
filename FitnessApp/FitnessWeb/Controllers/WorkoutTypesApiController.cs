using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;
using FitnessWeb.DTOs;

namespace FitnessWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutTypesApiController : ControllerBase
    {
        private readonly FitnessContext _context;

        public WorkoutTypesApiController(FitnessContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutType>>> GetTypes(string? difficulty)
        {
            var query = _context.WorkoutType.AsQueryable();
            if (!string.IsNullOrEmpty(difficulty)) query = query.Where(w => w.Difficulty == difficulty);
            return await query.ToListAsync();
        }

        [HttpGet("{id}/Sessions")]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessions(int id)
        {
            var sessions = await _context.Session
                .Include(s => s.Trainer)
                .Include(s => s.WorkoutType)
                .Include(s => s.Bookings)
                .Where(s => s.WorkoutTypeID == id && s.Date >= DateTime.Today)
                .OrderBy(s => s.Date).ThenBy(s => s.StartTime)
                .ToListAsync();

            return sessions.Select(s => new SessionDTO
            {
                ID = s.ID,
                Date = s.Date,
                StartTime = s.StartTime,
                WorkoutName = s.WorkoutType.Name,
                TrainerName = s.Trainer.FullName,
                AvailableSpots = s.MaxCapacity - (s.Bookings?.Count ?? 0)
            }).ToList();
        }
    }
}