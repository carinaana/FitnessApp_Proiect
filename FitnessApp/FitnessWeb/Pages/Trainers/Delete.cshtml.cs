using FitnessWeb.Data;
using FitnessWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FitnessWeb.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly FitnessContext _context;
        private readonly UserManager<IdentityUser> _userManager; 

        public DeleteModel(FitnessContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainer.FindAsync(id);

            if (trainer != null)
            {
                var user = await _userManager.FindByEmailAsync(trainer.Email);

                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }

                _context.Trainer.Remove(trainer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}