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

namespace FitnessWeb.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public IndexModel(FitnessWeb.Data.FitnessContext context)
        {
            _context = context;
        }

        public IList<Session> Session { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int? SelectedWorkoutTypeID { get; set; }
        public SelectList WorkoutTypesList { get; set; }
        public async Task OnGetAsync()
        {
            WorkoutTypesList = new SelectList(_context.WorkoutType, "ID", "Name");

            var query = _context.Session
                .Include(s => s.Trainer)
                .Include(s => s.WorkoutType)
                .AsQueryable();

           if (SelectedWorkoutTypeID.HasValue)
            {
                query = query.Where(s => s.WorkoutTypeID == SelectedWorkoutTypeID.Value);
            }

            Session = await query
                .OrderBy(s => s.Date)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        
        }
    }
}
