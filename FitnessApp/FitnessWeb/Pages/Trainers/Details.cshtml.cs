using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessWeb.Data;
using FitnessWeb.Models;

namespace FitnessWeb.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly FitnessContext _context;

        public DetailsModel(FitnessContext context)
        {
            _context = context;
        }

        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Trainer = await _context.Trainer
                .Include(t => t.TrainerSpecializations)
                .ThenInclude(ts => ts.WorkoutType)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Trainer == null) return NotFound();

            return Page();
        }
    }
}