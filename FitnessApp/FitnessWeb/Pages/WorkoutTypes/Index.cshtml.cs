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

namespace FitnessWeb.Pages.WorkoutTypes
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public IndexModel(FitnessWeb.Data.FitnessContext context)
        {
            _context = context;
        }

        public IList<WorkoutType> WorkoutType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            WorkoutType = await _context.WorkoutType.ToListAsync();
        }
    }
}
