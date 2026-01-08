using Microsoft.EntityFrameworkCore;
using FitnessWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FitnessWeb.Data
{
    public class FitnessContext : IdentityDbContext
    {
        public FitnessContext(DbContextOptions<FitnessContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Member { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<WorkoutType> WorkoutType { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<TrainerSpecialization> TrainerSpecialization { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
