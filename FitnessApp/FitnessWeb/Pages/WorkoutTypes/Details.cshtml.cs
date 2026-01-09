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
    public class DetailsModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public DetailsModel(FitnessWeb.Data.FitnessContext context)
        {
            _context = context;
        }

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
    }
}
