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
    public class IndexModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public IndexModel(FitnessWeb.Data.FitnessContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? SelectedSpecializationID { get; set; }

        public SelectList SpecializationsList { get; set; }

        public async Task OnGetAsync()
        {
            SpecializationsList = new SelectList(_context.WorkoutType, "ID", "Name");

            var query = _context.Trainer
                .Include(t => t.TrainerSpecializations)
                .ThenInclude(ts => ts.WorkoutType)
                .AsQueryable();

            if (SelectedSpecializationID.HasValue)
            {
                query = query.Where(t => t.TrainerSpecializations.Any(ts => ts.WorkoutTypeID == SelectedSpecializationID.Value));
            }

            Trainer = await query.ToListAsync();
        }
    }
}
