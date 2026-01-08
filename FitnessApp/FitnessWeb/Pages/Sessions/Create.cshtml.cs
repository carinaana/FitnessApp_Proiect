using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessWeb.Data;
using FitnessWeb.Models;

namespace FitnessWeb.Pages.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;

        public CreateModel(FitnessWeb.Data.FitnessContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TrainerID"] = new SelectList(_context.Trainer, "ID", "FullName");
        ViewData["WorkoutTypeID"] = new SelectList(_context.WorkoutType, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Session.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
