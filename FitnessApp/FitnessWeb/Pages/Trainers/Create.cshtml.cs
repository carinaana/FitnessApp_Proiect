using FitnessWeb.Data;
using FitnessWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessWeb.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : TrainerSpecializationsPageModel
    {
        private readonly FitnessWeb.Data.FitnessContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(FitnessContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            var trainer = new Trainer();
            trainer.TrainerSpecializations = new List<TrainerSpecialization>();

            PopulateAssignedSpecializationData(_context, trainer);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string[] selectedSpecializations)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser { UserName = Trainer.Email, Email = Trainer.Email };
            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Trainer");

                if (selectedSpecializations != null)
                {
                    Trainer.TrainerSpecializations = new List<TrainerSpecialization>();
                    foreach (var cat in selectedSpecializations)
                    {
                        var specToAdd = new TrainerSpecialization { WorkoutTypeID = int.Parse(cat) };
                        Trainer.TrainerSpecializations.Add(specToAdd);
                    }
                }

                _context.Trainer.Add(Trainer);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateAssignedSpecializationData(_context, Trainer); 

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
