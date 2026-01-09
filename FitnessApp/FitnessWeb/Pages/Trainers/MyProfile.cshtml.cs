using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;

namespace FitnessWeb.Pages.Trainers
{
    [Authorize(Roles = "Trainer")]
    public class MyProfileModel : TrainerSpecializationsPageModel 
    {
        private readonly FitnessContext _context;

        public MyProfileModel(FitnessContext context) => _context = context;

        [BindProperty]
        public Trainer Trainer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Trainer = await _context.Trainer
                .Include(t => t.TrainerSpecializations).ThenInclude(ts => ts.WorkoutType)
                .FirstOrDefaultAsync(t => t.Email == User.Identity.Name);

            if (Trainer == null) return NotFound("Profil negăsit.");

            PopulateAssignedSpecializationData(_context, Trainer);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedSpecializations)
        {
            var trainerToUpdate = await _context.Trainer
                .Include(t => t.TrainerSpecializations).ThenInclude(ts => ts.WorkoutType)
                .FirstOrDefaultAsync(t => t.Email == User.Identity.Name);

            if (trainerToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Trainer>(trainerToUpdate, "Trainer", t => t.Description))
            {
                UpdateTrainerSpecializations(_context, selectedSpecializations, trainerToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Details", new { id = trainerToUpdate.ID });
            }
            return Page();
        }
    }
}