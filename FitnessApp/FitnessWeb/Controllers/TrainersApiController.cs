using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;
using FitnessWeb.DTOs;

namespace FitnessWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersApiController : ControllerBase
    {
        private readonly FitnessContext _context;

        public TrainersApiController(FitnessContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDTO>>> GetTrainers(string? specialization)
        {
            var query = _context.Trainer
                .Include(t => t.TrainerSpecializations).ThenInclude(ts => ts.WorkoutType)
                .Include(t => t.Reviews)
                .AsQueryable();

            if (!string.IsNullOrEmpty(specialization))
            {
                query = query.Where(t => t.TrainerSpecializations.Any(ts => ts.WorkoutType.Name.Contains(specialization)));
            }

            var trainers = await query.ToListAsync();

            return trainers.Select(t => new TrainerDTO
            {
                ID = t.ID,
                FullName = t.FullName,
                Specialization = t.TrainerSpecializations != null
                    ? string.Join(", ", t.TrainerSpecializations.Select(ts => ts.WorkoutType.Name))
                    : "",
                Email = t.Email,
                Description = t.Description,
                AverageRating = t.Reviews != null && t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
                Reviews = t.Reviews?.Select(r => new ReviewDTO { Rating = r.Rating, Comment = r.Comment, MemberName = r.MemberName, Date = r.Date }).ToList()
            }).ToList();
        }

        [HttpPost("{id}/Review")]
        public async Task<IActionResult> PostReview(int id, [FromBody] ReviewDTO reviewDto)
        {
            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer == null) return NotFound("Trainer not found");

            var review = new Review
            {
                TrainerID = id,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                MemberName = reviewDto.MemberName,
                Date = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok("Review added");
        }
    }
}