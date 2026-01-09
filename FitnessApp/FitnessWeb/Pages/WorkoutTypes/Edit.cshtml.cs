using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;

namespace FitnessWeb.Pages.WorkoutTypes
{
    public class EditModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public EditModel(FitnessWeb.Data.FitnessContext context)
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

            var workouttype =  await _context.WorkoutType.FirstOrDefaultAsync(m => m.ID == id);
            if (workouttype == null)
            {
                return NotFound();
            }
            WorkoutType = workouttype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkoutType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutTypeExists(WorkoutType.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkoutTypeExists(int id)
        {
            return _context.WorkoutType.Any(e => e.ID == id);
        }
    }
}
