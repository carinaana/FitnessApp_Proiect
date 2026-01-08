using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;

namespace FitnessWeb.Pages.WorkoutTypes
{
    public class DeleteModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public DeleteModel(FitnessWeb.Data.FitnessContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkoutType WorkoutType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workouttype = await _context.WorkoutType.FirstOrDefaultAsync(m => m.ID == id);

            if (workouttype == null)
            {
                return NotFound();
            }
            else
            {
                WorkoutType = workouttype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workouttype = await _context.WorkoutType.FindAsync(id);
            if (workouttype != null)
            {
                WorkoutType = workouttype;
                _context.WorkoutType.Remove(WorkoutType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
