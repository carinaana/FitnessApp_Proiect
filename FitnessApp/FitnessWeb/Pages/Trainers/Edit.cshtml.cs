using FitnessWeb.Data;
using FitnessWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessWeb.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class EditModel : TrainerSpecializationsPageModel 
    {
        private readonly FitnessContext _context;

        public EditModel(FitnessContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();


            Trainer = await _context.Trainer
                .Include(t => t.TrainerSpecializations).ThenInclude(ts => ts.WorkoutType)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Trainer == null) return NotFound();

            PopulateAssignedSpecializationData(_context, Trainer);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedSpecializations)
        {
            if (id == null) return NotFound();

            var trainerToUpdate = await _context.Trainer
                .Include(t => t.TrainerSpecializations).ThenInclude(ts => ts.WorkoutType)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (trainerToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Trainer>(
                trainerToUpdate,
                "Trainer",
                t => t.FullName, t => t.Email, t => t.Description))
            {
                UpdateTrainerSpecializations(_context, selectedSpecializations, trainerToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedSpecializationData(_context, trainerToUpdate);
            return Page();
        }
    }
}
